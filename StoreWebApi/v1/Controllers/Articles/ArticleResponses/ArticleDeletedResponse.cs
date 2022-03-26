using Domain.Models;

namespace StoreWebApi.v1.Controllers.Articles.ArticleResponses
{
    public class ArticleDeletedResponse : ArticleResponse
    {
        public ArticleDeletedResponse()
            : base()
        {
        }

        public ArticleDeletedResponse(Article article)
            : base(article)
        {
        }

        public bool IsDeleted { get; set; }
    }
}
