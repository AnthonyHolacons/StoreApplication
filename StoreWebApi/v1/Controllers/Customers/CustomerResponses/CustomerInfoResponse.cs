using Domain.Models;

namespace StoreWebApi.v1.Controllers.Customers.CustomerResponses
{
    public class CustomerInfoResponse : CustomerResponse
    {
        public CustomerInfoResponse() : base()
        {
            Exist = true;
        }

        public CustomerInfoResponse(Customer customer)
            : base(customer)
        {
            CustomerId = customer.CustomerId;
            Dni = customer.Dni;
            Name = customer.Name;
            Surname = customer.LastName;
            Exist = true;
        }

        public string Dni { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool Exist { get; set; }
    }
}
