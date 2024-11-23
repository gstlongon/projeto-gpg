using Core.Models;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProducts(List<string> productsIds);
    }
}
