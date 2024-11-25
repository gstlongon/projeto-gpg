using Core.Models;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProduct(string productId);
        Task<List<Product>> GetProducts(List<string> productIds);
        Task<List<Product>> GetAllProducts();
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(string productId);
    }
}
