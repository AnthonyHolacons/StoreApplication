using Domain.Models;
using Domain.Models.Enums;

namespace StoreWebApi.v1.Controllers.Articles.ArticleResponses
{
    public class ArticleResponse
    {
        public ArticleResponse()
        {
        }

        public ArticleResponse(Article article)
        {
            ArticleId = article.ArticleId;
            ArticleType = (ArticleTypeEnum)article.ArticleTypeId;
        }

        public int? ArticleId { get; set; }

        public ArticleTypeEnum? ArticleType { get; set; }
    }
}
