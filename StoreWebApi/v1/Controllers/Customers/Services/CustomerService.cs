using Domain.Interfaces;
using Domain.Models;
using StoreWebApi.v1.Controllers.Customers.CustomerRequests;
using StoreWebApi.v1.Controllers.Customers.CustomerResponses;
using StoreWebApi.v1.Controllers.Customers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreWebApi.v1.Controllers.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IRepository<Customer> customerRepository,
                              IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public CustomerInfoResponse Get(int customerId)
        {
            var currentCustomer = _customerRepository.Get(customerId);
            if (currentCustomer != null)
            {
                return new CustomerInfoResponse(currentCustomer);
            }
            return new CustomerInfoResponse() { Exist = false };
        }

        public IEnumerable<CustomerInfoResponse> GetAll()
        {
            try
            {
                return from customer in _customerRepository.GetAll()
                       select new CustomerInfoResponse(customer);//save log info
            }
            catch (Exception)
            {
                //save log error
                throw;
            }
        }

        public CustomerDeletedResponse Remove(int customerId)
        {
            var customer = _customerRepository.Get(customerId);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
                _unitOfWork.SaveChanges();
                return new CustomerDeletedResponse(customer)
                {
                    IsDeleted = true
                };
                //save log delete
            }
            else
            {
                //save log cant delete
                return new CustomerDeletedResponse()
                {
                    IsDeleted = false
                };
            }
        }

        public CustomerResponse Submit(CustomerSubmitRequest customerRequest)
        {
            try
            {
                var customer = new Customer()
                {
                    Dni = customerRequest.Dni,
                    Name = customerRequest.Name,
                    LastName = customerRequest.Lastname
                };
                customer = _customerRepository.Add(customer);
                _unitOfWork.SaveChanges();
                //save info log
                return new CustomerResponse(customer);
            }
            catch (Exception)
            {
                //save error log
                throw;
            }
        }
    }
}
