using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Enums;
using Moq;
using NUnit.Framework;
using StoreWebApi.v1.Controllers.Articles.ArticleRequests;
using StoreWebApi.v1.Controllers.Articles.Services;
using StoreWebApi.v1.Controllers.Articles.Services.Intefaces;
using System;
using System.Collections.Generic;

namespace StoreWebApi.Test.v1.Controllers.Articles.Services
{
    public class ArticleServiceTests
    {
        private Mock<IRepository<Article>> _articleRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IArticleService _articleServiceSut;

        [SetUp]
        public void SetUp()
        {
            _articleRepositoryMock = new Mock<IRepository<Article>>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _articleServiceSut = new ArticleService(_articleRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public void Submit_WhenArticleRequestHasCorrectInfo_ThenSubmitSuccesss()
        {
            //Arrange
            var articleRequest = new ArticleSubmitRequest()
            {
                ArticleId = 1,
                ArticleType = ArticleTypeEnum.Apple,
                UnitPrice = 10M
            };
            var article = new Article()
            {
                ArticleId = articleRequest.ArticleId,
                ArticleTypeId = (short)articleRequest.ArticleType
            };
            _articleRepositoryMock.Setup(x => x.Add(It.IsAny<Article>())).Returns(article);
            //Act
            var result = _articleServiceSut.Submit(articleRequest);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(article.ArticleId, result.ArticleId);
            Assert.AreEqual(article.ArticleTypeId, (short)result.ArticleType);
        }

        [Test]
        public void Submit_WhenExceptionOnSavingChanges_ThenThrowException()
        {
            //Arrange
            var articleRequest = new ArticleSubmitRequest()
            {
                ArticleId = 2,
                ArticleType = ArticleTypeEnum.Orange,
                UnitPrice = 6.45M
            };
            var article = new Article()
            {
                ArticleId = 1,
                ArticleTypeId = 1
            };
            _articleRepositoryMock.Setup(x => x.Add(It.IsAny<Article>())).Returns(article);
            _unitOfWorkMock.Setup(x => x.SaveChanges()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _articleServiceSut.Submit(articleRequest));
        }

        [Test]
        public void GetAll_WhenData_ThenRetrieveList()
        {
            //Arrange
            var list = new List<Article>()
            {
                new Article()
                {
                    ArticleId = 2,
                    ArticleTypeId = 1,
                    UnitPrice = 10
                }
            };
            _articleRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _articleServiceSut.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_WhenNoData_ThenRetrieveEmptyList()
        {
            //Arrange
            var list = new List<Article>();
            _articleRepositoryMock.Setup(x => x.GetAll()).Returns(list);
            //Act            
            var result = _articleServiceSut.GetAll();
            //Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAll_WhenExceptionOnRetrieveData_ThenException()
        {
            //Arrange
            _articleRepositoryMock.Setup(x => x.GetAll()).Throws(new Exception());
            //Act & Assert
            Assert.Throws<Exception>(() => _articleServiceSut.GetAll());
        }

        [Test]
        public void Get_WhenCorrectId_ThenGetSuccess()
        {
            //Arrange
            var articleId = 3;
            var article = new Article()
            {
                ArticleId = articleId,
                ArticleTypeId = 1,
                UnitPrice = 1
            };
            _articleRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(article);
            //Act            
            var result = _articleServiceSut.Get(articleId);
            //Assert
            Assert.AreEqual(articleId, result.ArticleId);
        }

        [Test]
        public void Get_WhenIncorrectId_ThenGetArticleWrong()
        {
            //Arrange
            var articleId = 1;
            var article = new Article()
            {
                ArticleId = 3,
                ArticleTypeId = 1,
                UnitPrice = 1
            };
            //Act
            _articleRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(article);
            var result = _articleServiceSut.Get(articleId);
            //Assert
            Assert.AreNotEqual(articleId, result.ArticleId);
        }

        [Test]
        public void Remove_WhenArticleExist_ThenCorrectRemove()
        {
            //Arrange
            var articleId = 1;
            var article = new Article()
            {
                ArticleId = articleId,
                ArticleTypeId = 1,
                UnitPrice = 1
            };
            _articleRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(article);
            //Act
            var result = _articleServiceSut.Remove(articleId);
            //Assert
            Assert.AreEqual(result.IsDeleted, true);
        }

        [Test]
        public void Remove_WhenArticleDidntExist_ThenRemoveFalse()
        {
            //Arrange
            var articleId = 1;
            Article article = null;
            _articleRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(article);
            //Act
            var result = _articleServiceSut.Remove(articleId);
            //Assert
            Assert.AreEqual(result.IsDeleted, false);
        }
    }
}
