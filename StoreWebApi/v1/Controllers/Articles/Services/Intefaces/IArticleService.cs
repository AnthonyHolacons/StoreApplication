using StoreWebApi.v1.Controllers.Articles.ArticleRequests;
using StoreWebApi.v1.Controllers.Articles.ArticleResponses;
using System.Collections.Generic;

namespace StoreWebApi.v1.Controllers.Articles.Services.Intefaces
{
    public interface IArticleService
    {
        ArticleResponse Submit(ArticleSubmitRequest articleRequest);

        IEnumerable<ArticleInfoResponse> GetAll();

        ArticleInfoResponse Get(int articleId);

        ArticleDeletedResponse Remove(int articleId);
    }
}
