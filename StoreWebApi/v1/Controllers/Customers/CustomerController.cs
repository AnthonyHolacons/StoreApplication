using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.v1.Controllers.Customers.CustomerRequests;
using StoreWebApi.v1.Controllers.Customers.CustomerResponses;
using StoreWebApi.v1.Controllers.Customers.Services.Interfaces;
using System.Collections.Generic;

namespace StoreWebApi.v1.Controllers.Customers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<CustomerResponse> Submit([FromBody] CustomerSubmitRequest customerSubmitRequest)
        {
            var result = _customerService.Submit(customerSubmitRequest);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerInfoResponse>> GetAll()
        {
            var result = _customerService.GetAll();
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> GetPurchase(int customerId)
        {
            var result = _customerService.Get(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CustomerResponse> Remove(int customerId)
        {
            var result = _customerService.Remove(customerId);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
