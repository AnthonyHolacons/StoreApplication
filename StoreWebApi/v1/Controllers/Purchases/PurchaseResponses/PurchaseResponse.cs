using Domain.Models;

namespace StoreWebApi.v1.Controllers.Purchases.PurchaseResponses
{
    public class PurchaseResponse
    {
        public PurchaseResponse()
        {

        }

        public PurchaseResponse(Purchase purchase)
        {
            PurchaseId = purchase.PurchaseId;
        }

        public int PurchaseId { get; set; }
    }
}
