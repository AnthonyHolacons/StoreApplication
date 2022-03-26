using StoreWebApi.v1.Controllers.Purchases.PurchaseRequests;
using StoreWebApi.v1.Controllers.Purchases.PurchaseResponses;
using System.Collections.Generic;

namespace StoreWebApi.v1.Controllers.Purchases.Services.Interfaces
{
    public interface IPurchaseService
    {
        PurchaseResponse Submit(PurchaseSubmitRequest purchaseRequest);

        IEnumerable<PurchaseInfoResponse> GetAll();

        PurchaseInfoResponse Get(int purchaseId);

        PurchaseDeletedResponse Remove(int purchaseId);
    }
}
