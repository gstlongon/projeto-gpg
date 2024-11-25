using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersDbContext _context;

        public ProductRepository(OrdersDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetProduct(string productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<Product>> GetProducts(List<string> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product; 
        }

        public async Task UpdateProduct(Product product)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(string productId)
        {
            var product = await GetProduct(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}
