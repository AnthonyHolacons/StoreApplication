using System.Collections.Generic;

namespace Domain.Models
{
    public class Article
    {
        public Article()
        {
            Purchase = new HashSet<Purchase>();
        }

        public int ArticleId { get; set; }
        public int ArticleTypeId { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual ArticleType ArticleType { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
