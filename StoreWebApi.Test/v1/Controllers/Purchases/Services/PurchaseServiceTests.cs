using Domain.Interfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;
using StoreWebApi.v1.Controllers.Purchases.PurchaseRequests;
using StoreWebApi.v1.Controllers.Purchases.Services;
using StoreWebApi.v1.Controllers.Purchases.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace StoreWebApi.Test.v1.Controllers.Purchases.Services
{
    public class PurchaseServiceTests
    {
        private Mock<IRepository<Purchase>> _purchaseRepositoryMock;
        private Mock<IRepository<Article>> _articleRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IPurchaseService _purchaseServiceMock;

        [SetUp]
        public void SetUp()
        {
            _purchaseRepositoryMock = new Mock<IRepository<Purchase>>();
            _articleRepositoryMock = new Mock<IRepository<Article>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _purchaseServiceMock = new PurchaseService(_purchaseRepositoryMock.Object,
                                                      _articleRepositoryMock.Object,
                                                      _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenArticleRequestHasCorrectInfo_ThenSubmitSuccesss()
        {
            //Arrange
            var purchaseRequest = new PurchaseSubmitRequest()
            {
                ArticleId = 1,
                CustomerId = 1,
                Quantity = 2
            };
            var purchase = new Purchase()
            {
                PurchaseId = 1,
                ArticleId = purchaseRequest.ArticleId,
                CustomerId = purchaseRequest.CustomerId,
                Quantity = purchaseRequest.Quantity
            };
            _purchaseRepositoryMock.Setup(x => x.Add(It.IsAny<Purchase>())).Returns(purchase);
            //Act
            var result = _purchaseServiceMock.Submit(purchaseRequest);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(purchase.PurchaseId, result.PurchaseId);
        }

        [Test]
        public void Submit_WhenExceptionOnSavingChanges_ThenThrowException()
        {
            //Arrange
            var purchaseRequest = new PurchaseSubmitRequest();
            var purchase = new Purchase()
            {
                PurchaseId = 1,
                ArticleId = purchaseRequest.ArticleId,
                CustomerId = purchaseRequest.CustomerId,
                Quantity = purchaseRequest.Quantity
            };
            _purchaseRepositoryMock.Setup(x => x.Add(It.IsAny<Purchase>())).Returns(purchase);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _purchaseServiceMock.Submit(purchaseRequest));
        }

        [Test]
        public void GetAll_WhenData_ThenRetrieveList()
        {
            //Arrange
            var list = new List<Purchase>()
            {
                new Purchase()
                {
                    PurchaseId = 1,
                    ArticleId = 1,
                    CustomerId = 1,
                    Quantity = 1
                }
            };
            _purchaseRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _purchaseServiceMock.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenRetrieveEmptyList()
        {
            //Arrange
            var list = new List<Purchase>();
            _purchaseRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _purchaseServiceMock.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionOnRetrieveData_ThenException()
        {
            //Arrange
            _purchaseRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _purchaseServiceMock.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSuccess()
        {
            //Arrange
            var purchaseId = 3;
            var purchase = new Purchase()
            {
                PurchaseId = purchaseId,
                ArticleId = 1,
                CustomerId = 1,
                Quantity = 1
            };
            _purchaseRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(purchase);
            //Act            
            var result = _purchaseServiceMock.Get(purchaseId);
            //Assert
            Assert.AreEqual(purchase.PurchaseId, result.PurchaseId);
        }

        [Test]
        public void Get_WhenIncorrectId_ThenGetArticleWrong()
        {
            //Arrange
            var purchaseId = 1;
            Purchase purchase = null;
            //Act
            _purchaseRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(purchase);
            var result = _purchaseServiceMock.Get(purchaseId);
            //Assert
            Assert.AreEqual(result.Exist, false);
        }

        [Test]
        public void Remove_WhenArticleExist_ThenCorrectRemove()
        {
            //Arrange
            var purchaseId = 1;
            var purchase = new Purchase()
            {
                PurchaseId = 1,
                ArticleId = 1,
                CustomerId = 1,
                Quantity = 1
            };
            _purchaseRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(purchase);
            //Act
            var result = _purchaseServiceMock.Remove(purchaseId);
            //Assert
            Assert.AreEqual(result.IsDeleted, true);
        }

        [Test]
        public void Remove_WhenArticleDidntExist_ThenRemoveFalse()
        {
            //Arrange
            var purchaseId = 1;
            Purchase purchase = null;
            _purchaseRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(purchase);
            //Act
            var result = _purchaseServiceMock.Remove(purchaseId);
            //Assert
            Assert.AreEqual(result.IsDeleted, false);
        }
    }
}
