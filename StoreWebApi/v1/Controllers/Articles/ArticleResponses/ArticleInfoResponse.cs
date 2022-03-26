using Domain.Models;

namespace StoreWebApi.v1.Controllers.Articles.ArticleResponses
{
    public class ArticleInfoResponse : ArticleResponse
    {
        public ArticleInfoResponse() 
            : base()
        {
            Exist = true;
        }
        public ArticleInfoResponse(Article article)
            : base(article)
        {
            UnitPrice = (decimal)article.UnitPrice;
            Exist = true;
        }
        public decimal UnitPrice { get; set; }

        public bool Exist { get; set; }
    }
}
