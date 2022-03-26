using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly StoreDbContext _storeDbContext;

        public CustomerRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public Customer Add(Customer entity)
        {
            return _storeDbContext.Customer.Add(entity).Entity;
        }

        public void Delete(Customer entity)
        {            
            _storeDbContext.Customer.Remove(entity);
        }

        public Customer Get(int id)
        {
            return _storeDbContext.Customer.FirstOrDefault(x => x.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _storeDbContext.Customer;
        }
    }
}
