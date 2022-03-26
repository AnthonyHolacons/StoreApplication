using StoreWebApi.v1.Controllers.Customers.CustomerRequests;
using StoreWebApi.v1.Controllers.Customers.CustomerResponses;
using System.Collections.Generic;

namespace StoreWebApi.v1.Controllers.Customers.Services.Interfaces
{
    public interface ICustomerService
    {
        CustomerResponse Submit(CustomerSubmitRequest customerRequest);

        IEnumerable<CustomerInfoResponse> GetAll();

        CustomerInfoResponse Get(int customerId);

        CustomerDeletedResponse Remove(int customerId);
    }
}
