using Domain.Models;

namespace StoreWebApi.v1.Controllers.Customers.CustomerResponses
{
    public class CustomerResponse
    {
        public CustomerResponse()
        {
        }

        public CustomerResponse(Customer customer)
        {
            CustomerId = customer.CustomerId;
        }

        public int? CustomerId { get; set; }
    }
}
