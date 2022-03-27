using Domain.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;
using StoreWebApi.v1.Controllers.Customers.CustomerRequests;
using StoreWebApi.v1.Controllers.Customers.Services;
using StoreWebApi.v1.Controllers.Customers.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace StoreWebApi.Test.v1.Controllers.Customers.Services
{
    public class CustomerServiceTests
    {
        private Mock<IRepository<Customer>> _customerRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ICustomerService _customerServiceSut;

        [SetUp]
        public void SetUp()
        {
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _customerServiceSut = new CustomerService(_customerRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenArticleRequestHasCorrectInfo_ThenSubmitSuccesss()
        {
            //Arrange
            var customerRequest = new CustomerSubmitRequest()
            {
                Dni = "dni",
                Name = "name",
                Lastname = "lastname"
            };
            var customer = new Customer()
            {
                CustomerId = 1,
                Dni = customerRequest.Dni,
                Name = customerRequest.Name,
                LastName = customerRequest.Lastname
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);
            //Act
            var result = _customerServiceSut.Submit(customerRequest);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customer.CustomerId, result.CustomerId);
        }

        [Test]
        public void Submit_WhenExceptionOnSavingChanges_ThenThrowException()
        {
            //Arrange
            var customerRequest = new CustomerSubmitRequest();
            var customer = new Customer()
            {
                CustomerId = 1,
                Dni = customerRequest.Dni,
                Name = customerRequest.Name,
                LastName = customerRequest.Lastname
            };
            _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Customer>())).Returns(customer);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _customerServiceSut.Submit(customerRequest));
        }

        [Test]
        public void GetAll_WhenData_ThenRetrieveList()
        {
            //Arrange
            var list = new List<Customer>()
            {
                new Customer()
                {
                    Dni = "dni",
                    Name = "name",
                    LastName = "lastname"
                }
            };
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _customerServiceSut.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenRetrieveEmptyList()
        {
            //Arrange
            var list = new List<Customer>();
            _customerRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _customerServiceSut.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionOnRetrieveData_ThenException()
        {
            //Arrange
            _customerRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _customerServiceSut.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSuccess()
        {
            //Arrange
            var customerId = 3;
            var customer = new Customer()
            {
                CustomerId = customerId,
                Dni = "dni",
                Name = "name",
                LastName = "lastname"
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            //Act            
            var result = _customerServiceSut.Get(customerId);
            //Assert
            Assert.AreEqual(customerId, result.CustomerId);
        }

        [Test]
        public void Get_WhenIncorrectId_ThenGetArticleWrong()
        {
            //Arrange
            var customerId = 1;
            Customer customer = null;
            //Act
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            var result = _customerServiceSut.Get(customerId);
            //Assert
            Assert.AreEqual(result.Exist, false);
        }

        [Test]
        public void Remove_WhenArticleExist_ThenCorrectRemove()
        {
            //Arrange
            var customerId = 1;
            var customer = new Customer()
            {
                CustomerId = customerId,
                Dni = "dni",
                Name = "name",
                LastName = "lastname"
            };
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            //Act
            var result = _customerServiceSut.Remove(customerId);
            //Assert
            Assert.AreEqual(result.IsDeleted, true);
        }

        [Test]
        public void Remove_WhenArticleDidntExist_ThenRemoveFalse()
        {
            //Arrange
            var customerId = 1;
            Customer customer = null;
            _customerRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(customer);
            //Act
            var result = _customerServiceSut.Remove(customerId);
            //Assert
            Assert.AreEqual(result.IsDeleted, false);
        }
    }
}
