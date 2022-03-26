using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.v1.Controllers.Purchases.PurchaseRequests;
using StoreWebApi.v1.Controllers.Purchases.PurchaseResponses;
using StoreWebApi.v1.Controllers.Purchases.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApi.v1.Controllers.Purchases
{
    [Route("api/v1/purchases")]
    [ApiController]
    public class PurchaseController
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<PurchaseResponse> Submit([FromBody] PurchaseSubmitRequest purchaseSubmitRequest)
        {
            var result = _purchaseService.Submit(purchaseSubmitRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PurchaseInfoResponse>> GetAll()
        {
            var result = _purchaseService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PurchaseResponse> GetPurchase(int purchaseId)
        {
            var result = _purchaseService.Get(purchaseId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<PurchaseResponse> Remove(int purchaseId)
        {
            var result = _purchaseService.Remove(purchaseId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
