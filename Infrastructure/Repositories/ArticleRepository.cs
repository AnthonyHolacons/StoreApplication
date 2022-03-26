using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly StoreDbContext _storeDbContext;

        public ArticleRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public Article Add(Article entity)
        {
            return _storeDbContext.Article.Add(entity).Entity;
        }

        public void Delete(Article entity)
        {
            _storeDbContext.Article.Remove(entity);
        }

        public Article Get(int id)
        {
            return _storeDbContext.Article.FirstOrDefault(x => x.ArticleId == id);
        }

        public IEnumerable<Article> GetAll()
        {
            return _storeDbContext.Article;
        }

    }
}
