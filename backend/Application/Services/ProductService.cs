using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;


        public ProductService(IProductRepository productRepository, IImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product?> GetProduct(string productId)
        {
            return await _productRepository.GetProduct(productId);
        }

        public async Task<Product> GetProductOrThrowException(string productId)
        {
            Product? product = await GetProduct(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<Product> CreateProduct(string name, string description, double price)
        {

            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Description = description,
                Price = price
            };

            await _productRepository.AddProduct(product);
            return product;
        }

        public async Task<Product> UpdateProduct(string productId, string name, string description, double price)
        {
            var product = await GetProductOrThrowException(productId);

            product.Name = name;
            product.Description = description;
            product.Price = price;

            await _productRepository.UpdateProduct(product);

            return product;
        }

        public async Task DeleteProduct(string productId)
        {
            await _productRepository.DeleteProduct(productId);
        }

        public async Task<string> UploadProductImage(string productId, FileData file)
        {
            Product product = await GetProduct(productId);

            string uploadedFileUrl = await _imageService.UploadImage(file, "products", productId);

                product.ImageUrl = uploadedFileUrl;

            await _productRepository.UpdateProduct(product);

            return uploadedFileUrl;
        }
        
    }
}
