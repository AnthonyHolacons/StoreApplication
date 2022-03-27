using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Infrastructure.Tests.Repositories
{
    public class PurchaseRepositoryTests
    {
        private StoreDbContext myContext;

        [SetUp]
        public void Setup()
        {
            myContext = new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase("myDatabase").Options);
        }

        [Test]
        public void Add_WhenHasData_ThenPurchaseAdded()
        {
            using (myContext)
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 1,
                    CustomerId = 1,
                    ArticleId = 1,
                    Quantity = 5
                };
                IRepository<Purchase> purchaseRepositorySut = new PurchaseRepository(myContext);
                //act
                var result = purchaseRepositorySut.Add(purchase);
                myContext.SaveChanges();
                //assert
                result = myContext.Purchase.First(x => x.PurchaseId == purchase.PurchaseId);
                Assert.IsNotNull(result);
                Assert.AreEqual(purchase.PurchaseId, result.PurchaseId);
                Assert.AreEqual(purchase.CustomerId, result.CustomerId);
                Assert.AreEqual(purchase.ArticleId, result.ArticleId);
                Assert.AreEqual(purchase.Quantity, result.Quantity);

            }
        }

        [Test]
        public void Delete_WhenPurchaseExist_ThenPurchaseDeleted()
        {
            using (myContext)
            {
                //arrange
                var purchaseToRemoveId = 2;
                var purchase = new Purchase()
                {
                    PurchaseId = purchaseToRemoveId
                };
                IRepository<Purchase> purchaseRespositorySut = new PurchaseRepository(myContext);
                purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();
                var currentPurchase = purchaseRespositorySut.Get(purchaseToRemoveId);
                //act
                purchaseRespositorySut.Delete(currentPurchase);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(purchaseRespositorySut.Get(purchaseToRemoveId));
            }
        }

        [Test]
        public void GetAll_WhenData_ThenFilledList()
        {
            using (myContext)
            {
                //arrange
                var purchase = new Purchase()
                {
                    PurchaseId = 3
                };
                IRepository<Purchase> purchaseRespositorySut = new PurchaseRepository(myContext);
                purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();
                //act
                var purchaseList = purchaseRespositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsNotEmpty(purchaseList);
            }
        }

        [Test]
        public void GetAll_WhenNoData_ThenEmptyList()
        {
            using (myContext)
            {
                //arrange
                IRepository<Purchase> purchaseRepositorySut = new PurchaseRepository(myContext);
                foreach (var purchase in purchaseRepositorySut.GetAll())
                {
                    purchaseRepositorySut.Delete(purchase);
                }
                //act
                var purchaseList = purchaseRepositorySut.GetAll();
                myContext.SaveChanges();
                //assert
                Assert.IsEmpty(purchaseList);
            }
        }

        [Test]
        public void GetById_WhenCustomerFound_ThenRetrieveCustomer()
        {
            using (myContext)
            {
                //arrange
                var purchaseId = 4;
                var purchase = new Purchase()
                {
                    PurchaseId = 4
                };
                IRepository<Purchase> purchaseRespositorySut = new PurchaseRepository(myContext);
                purchaseRespositorySut.Add(purchase);
                myContext.SaveChanges();
                //act
                var purchaseFound = purchaseRespositorySut.Get(purchaseId);
                myContext.SaveChanges();
                //assert
                Assert.IsNotNull(purchaseFound);
            }
        }

        [Test]
        public void GetById_WhenNoData_ThenRetrieveNull()
        {
            using (myContext)
            {
                //arrange
                var purchaseId = -1;
                IRepository<Purchase> purchaseRepositorySut = new PurchaseRepository(myContext);
                //act
                var purchaseFound = purchaseRepositorySut.Get(purchaseId);
                myContext.SaveChanges();
                //assert
                Assert.IsNull(purchaseFound);
            }
        }
    }
}
