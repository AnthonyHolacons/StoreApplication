namespace StoreWebApi.v1.Controllers.Purchases.PurchaseRequests
{
    public class PurchaseSubmitRequest
    {

        public int CustomerId { get; set; }

        public int ArticleId { get; set; }

        public int Quantity { get; set; }
    }
}
