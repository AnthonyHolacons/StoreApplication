using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private readonly StoreDbContext _storeDbContext;

        public PurchaseRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public Purchase Add(Purchase entity)
        {
            return _storeDbContext.Purchase.Add(entity).Entity;
        }

        public void Delete(Purchase entity)
        {            
            _storeDbContext.Purchase.Remove(entity);
        }

        public Purchase Get(int id)
        {
            return _storeDbContext.Purchase.FirstOrDefault(x => x.PurchaseId == id);
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _storeDbContext.Purchase;
        }
    }
}
