using Core.Models;

namespace Core.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProduct(string productId);
        Task<Product> GetProductOrThrowException(string productId);
        Task<Product> CreateProduct(string name, string description, double price);
        Task<string> UploadProductImage(string productId, FileData file);

        Task<Product> UpdateProduct(string productId, string name, string description, double price);

        Task DeleteProduct(string productId);
    }
}
