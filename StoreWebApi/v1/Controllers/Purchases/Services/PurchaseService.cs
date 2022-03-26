using Domain.Interfaces;
using Domain.Models;
using StoreWebApi.v1.Controllers.Articles.Services;
using StoreWebApi.v1.Controllers.Purchases.PurchaseRequests;
using StoreWebApi.v1.Controllers.Purchases.PurchaseResponses;
using StoreWebApi.v1.Controllers.Purchases.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreWebApi.v1.Controllers.Purchases.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IRepository<Purchase> purchaseRepository,
                               IRepository<Article> articleRepository,
                               IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public PurchaseInfoResponse Get(int purchaseId)
        {
            var currentPurchase = _purchaseRepository.Get(purchaseId);
            if (currentPurchase != null)
            {
                return new PurchaseInfoResponse(currentPurchase);
            }
            return new PurchaseInfoResponse() { Exist = false };
        }

        public IEnumerable<PurchaseInfoResponse> GetAll()
        {
            try
            {
                return from purchase in _purchaseRepository.GetAll()
                       select new PurchaseInfoResponse(purchase);//save log info
            }
            catch (Exception)
            {
                //save log error
                throw;
            }
        }

        public PurchaseDeletedResponse Remove(int purchaseId)
        {
            var customer = _purchaseRepository.Get(purchaseId);
            if (customer != null)
            {
                _purchaseRepository.Delete(customer);
                _unitOfWork.SaveChanges();
                return new PurchaseDeletedResponse(customer)
                {
                    IsDeleted = true
                };
                //save log delete
            }
            else
            {
                //save log cant delete
                return new PurchaseDeletedResponse()
                {
                    IsDeleted = false
                };
            }
        }

        public PurchaseResponse Submit(PurchaseSubmitRequest purchaseRequest)
        {
            try
            {
                var purchase = new Purchase()
                {
                    CustomerId = purchaseRequest.CustomerId,
                    ArticleId = purchaseRequest.ArticleId,
                    Quantity = purchaseRequest.Quantity,
                    TotalPrice = GetTotalPrice(purchaseRequest.ArticleId,
                                               purchaseRequest.Quantity)
                };
                purchase = _purchaseRepository.Add(purchase);
                _unitOfWork.SaveChanges();
                //save info log
                return new PurchaseResponse(purchase);
            }
            catch (Exception)
            {
                //save error log
                throw;
            }
        }

        private decimal? GetTotalPrice(int articleId, int quantity)
        {
            var article = _articleRepository.Get(articleId);
            if (article != null)
            {
                return (decimal)(article.UnitPrice * quantity);
            }
            else
            {
                return 0L;
            }
        }
    }
}
