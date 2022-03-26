using Domain.Interfaces;
using Domain.Models;
using StoreWebApi.v1.Controllers.Articles.ArticleRequests;
using StoreWebApi.v1.Controllers.Articles.ArticleResponses;
using StoreWebApi.v1.Controllers.Articles.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreWebApi.v1.Controllers.Articles.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleService(IRepository<Article> articleRepository,
                              IUnitOfWork unitOfWork)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
        }

        public ArticleInfoResponse Get(int articleId)
        {
            var currentArticle = _articleRepository.Get(articleId);
            if (currentArticle != null)
            {
                return new ArticleInfoResponse(currentArticle);
            }
            return new ArticleInfoResponse() { Exist = false };
        }

        public IEnumerable<ArticleInfoResponse> GetAll()
        {
            try
            {
                return from article in _articleRepository.GetAll()
                       select new ArticleInfoResponse(article);//save log info
            }
            catch (Exception)
            {
                //save log error
                throw;
            }
        }

        public ArticleDeletedResponse Remove(int articleId)
        {
            var article = _articleRepository.Get(articleId);
            if (article != null)
            {
                _articleRepository.Delete(article);
                _unitOfWork.SaveChanges();
                return new ArticleDeletedResponse(article)
                {
                    IsDeleted = true
                };
                //save log delete
            }
            else
            {
                //save log cant delete
                return new ArticleDeletedResponse()
                {
                    IsDeleted = false
                };
            }

        }

        public ArticleResponse Submit(ArticleSubmitRequest articleRequest)
        {
            try
            {
                var article = new Article()
                {
                    ArticleId = articleRequest.ArticleId,
                    ArticleTypeId = (short)articleRequest.ArticleType,
                    UnitPrice = articleRequest.UnitPrice
                };
                article = _articleRepository.Add(article);
                _unitOfWork.SaveChanges();
                //save info log
                return new ArticleResponse(article);
            }
            catch (Exception)
            {
                //save error log
                throw;
            }
        }
    }
}
