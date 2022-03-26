using Domain.Models;

namespace StoreWebApi.v1.Controllers.Purchases.PurchaseResponses
{
    public class PurchaseDeletedResponse : PurchaseResponse
    {
        public PurchaseDeletedResponse()
            : base()
        {

        }

        public PurchaseDeletedResponse(Purchase purchase)
            : base(purchase)
        {

        }

        public bool IsDeleted { get; set; }
    }
}
