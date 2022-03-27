using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repositories
{
    public class ArticleRepositoryTests
    {
        private StoreDbContext myContext;

        [SetUp]
        public void Setup()
        {
            myContext = new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenArticleAdded()
        {
            using (myContext)
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 1,
                    ArticleTypeId = 1,
                    UnitPrice = 3
                };
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                //act
                var result = articleRespositorySut.Add(article);
                myContext.SaveChanges();
                //assert
                result = myContext.Article.First(x => x.ArticleId == article.ArticleId);
                Assert.IsNotNull(result);
                Assert.AreEqual(article.ArticleId, result.ArticleId);
                Assert.AreEqual(article.ArticleType, result.ArticleType);
                Assert.AreEqual(article.UnitPrice, result.UnitPrice);
            }
        }

        [Test]
        public void Delete_WhenData_ThenArticleDeleted()
        {
            using (myContext)
            {
                //arrange
                var articleToRemoveId = 2;
                var article = new Article()
                {
                    ArticleId = articleToRemoveId,
                    ArticleTypeId = 2
                };
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                articleRespositorySut.Add(article);
                myContext.SaveChanges();
                var currentArticle = articleRespositorySut.GetAll().FirstOrDefault(x => x.ArticleId == articleToRemoveId);
                //act
                articleRespositorySut.Delete(currentArticle);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(articleRespositorySut.Get(articleToRemoveId));
            }
        }

        [Test]
        public void GetAll_WhenData_ThenFilledList()
        {
            using (myContext)
            {
                //arrange
                var article = new Article()
                {
                    ArticleId = 3,
                    UnitPrice = 3
                };
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                articleRespositorySut.Add(article);
                myContext.SaveChanges();
                //act
                var articleList = articleRespositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsNotEmpty(articleList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (myContext)
            {
                //arrange
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                foreach (var article in articleRespositorySut.GetAll())
                {
                    articleRespositorySut.Delete(article);
                }
                //act
                var articleList = articleRespositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsEmpty(articleList);
            }
        }

        [Test]
        public void GetById_WhenData_ThenRetrieveArticle()
        {
            using (myContext)
            {
                //arrange
                var articleId = 4;
                var article = new Article()
                {
                    ArticleId = 4,
                    UnitPrice = 3
                };
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                articleRespositorySut.Add(article);
                myContext.SaveChanges();
                //act
                var articleFound = articleRespositorySut.Get(articleId);
                myContext.SaveChanges();
                //assert
                Assert.IsNotNull(articleFound);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNullObject()
        {
            using (myContext)
            {
                //arrange
                var articleId = -1;
                IRepository<Article> articleRespositorySut = new ArticleRepository(myContext);
                //act
                var articleFound = articleRespositorySut.Get(articleId);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(articleFound);
            }
        }
    }
}
