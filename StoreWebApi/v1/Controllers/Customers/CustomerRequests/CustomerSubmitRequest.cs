using System.ComponentModel.DataAnnotations;

namespace StoreWebApi.v1.Controllers.Customers.CustomerRequests
{
    public class CustomerSubmitRequest
    {
        [Required]
        public string Dni { get; set; }

        [Required]
        public string Name { get; set; }

        public string Lastname { get; set; }
    }
}
