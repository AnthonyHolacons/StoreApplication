using Domain.Models;

namespace StoreWebApi.v1.Controllers.Purchases.PurchaseResponses
{
    public class PurchaseInfoResponse : PurchaseResponse
    {
        public PurchaseInfoResponse()
            : base()
        {
            Exist = true;
        }

        public PurchaseInfoResponse(Purchase purchase)
            : base(purchase)
        {
            PurchaseId = purchase.PurchaseId;
            CustomerId = purchase.CustomerId;
            ArticleId = purchase.ArticleId;
            Quantity = purchase.Quantity;
            TotalPrice = purchase.TotalPrice;
            Exist = true;
        }

        public int CustomerId { get; set; }

        public int ArticleId { get; set; }

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public bool Exist { get; set; }
    }
}
