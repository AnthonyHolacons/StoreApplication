using Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.v1.Controllers.Articles.ArticleRequests
{
    public class ArticleSubmitRequest
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public ArticleTypeEnum ArticleType { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
