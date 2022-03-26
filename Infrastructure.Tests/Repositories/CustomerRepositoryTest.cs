using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repositories
{
    public class CustomerRepositoryTest
    {
        private StoreDbContext myContext;

        [SetUp]
        public void Setup()
        {
            myContext = new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenCustomerAdded()
        {
            using (myContext)
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 1,
                    Dni = "dni",
                    Name = "name",
                    LastName = "lastname"
                };
                IRepository<Customer> customerRepositorySut = new CustomerRepository(myContext);
                //act
                var result = customerRepositorySut.Add(customer);
                myContext.SaveChanges();
                //assert
                result = myContext.Customer.First(x => x.CustomerId == customer.CustomerId);
                Assert.IsNotNull(result);
                Assert.AreEqual(customer.CustomerId, result.CustomerId);
                Assert.AreEqual(customer.Name, result.Name);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Dni, result.Dni);
            }
        }

        [Test]
        public void Delete_WhenCustomerExist_ThenCustomerDeleted()
        {
            using (myContext)
            {
                //arrange
                var customerToRemoveId = 2;
                var customer = new Customer()
                {
                    CustomerId = customerToRemoveId
                };
                IRepository<Customer> customerRespositorySut = new CustomerRepository(myContext);
                customerRespositorySut.Add(customer);
                myContext.SaveChanges();
                var currentCustomer = customerRespositorySut.Get(customerToRemoveId);
                //act
                customerRespositorySut.Delete(currentCustomer);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(customerRespositorySut.Get(customerToRemoveId));
            }
        }

        [Test]
        public void GetAll_WhenData_ThenFilledList()
        {
            using (myContext)
            {
                //arrange
                var customer = new Customer()
                {
                    CustomerId = 3
                };
                IRepository<Customer> customerRepositorySut = new CustomerRepository(myContext);
                customerRepositorySut.Add(customer);
                myContext.SaveChanges();
                //act
                var customerList = customerRepositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsNotEmpty(customerList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (myContext)
            {
                //arrange
                IRepository<Customer> customerRepositorySut = new CustomerRepository(myContext);
                foreach (var customer in customerRepositorySut.GetAll())
                {
                    customerRepositorySut.Delete(customer);
                }
                //act
                var customerList = customerRepositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsEmpty(customerList);
            }
        }

        [Test]
        public void GetById_WhenCustomerFound_ThenRetrieveCustomer()
        {
            using (myContext)
            {
                //arrange
                var customerId = 4;
                var customer = new Customer()
                {
                    CustomerId = 4
                };
                IRepository<Customer> customerRespositorySut = new CustomerRepository(myContext);
                customerRespositorySut.Add(customer);
                myContext.SaveChanges();
                //act
                var customerFound = customerRespositorySut.Get(customerId);
                myContext.SaveChanges();
                //assert
                Assert.IsNotNull(customerFound);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNull()
        {
            using (myContext)
            {
                //arrange
                var customerId = -1;
                IRepository<Customer> customerRepositorySut = new CustomerRepository(myContext);
                //act
                var customerFound = customerRepositorySut.Get(customerId);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(customerFound);
            }
        }
    }
}
