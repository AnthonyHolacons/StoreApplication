using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApi.v1.Controllers.Customers.CustomerResponses
{
    public class CustomerDeletedResponse : CustomerResponse
    {
        public CustomerDeletedResponse()
            :base()
        {

        }
        public CustomerDeletedResponse(Customer customer)
            : base(customer)
        {
            CustomerId = customer.CustomerId;
        }

        public bool IsDeleted { get; set; }
    }
}
