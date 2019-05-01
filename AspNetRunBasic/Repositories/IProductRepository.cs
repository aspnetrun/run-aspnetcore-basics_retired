using AspNetRunBasic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetRunBasic.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId);
    }
}
