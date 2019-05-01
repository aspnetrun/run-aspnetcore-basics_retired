using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetRunBasic.Data;
using AspNetRunBasic.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetRunBasic.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly AspnetRunContext _dbContext;

        public ProductRepository(AspnetRunContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            return await _dbContext.Products
                    .Where(p => p.Name.Contains(name))
                    .OrderBy(p => p.Name)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        

        public Task<Product> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
