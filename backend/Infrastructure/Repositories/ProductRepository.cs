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

        public async Task<List<Product>> GetProducts(List<string> productsIds)
        {
            List<Product> products = await _context
               .Products
               .Where(p => productsIds.Contains(p.Id))
               .ToListAsync();

            return products;
        }

    }
}
