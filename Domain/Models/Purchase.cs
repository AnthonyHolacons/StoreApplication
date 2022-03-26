namespace Domain.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public int ArticleId { get; set; }
        public int CustomerId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }

        public virtual Article Article { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
