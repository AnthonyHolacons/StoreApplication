using System.Collections.Generic;

namespace Domain.Models
{
    public class ArticleType
    {
        public ArticleType()
        {
            Article = new HashSet<Article>();
        }

        public int ArticleTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Article> Article { get; set; }
    }
}
