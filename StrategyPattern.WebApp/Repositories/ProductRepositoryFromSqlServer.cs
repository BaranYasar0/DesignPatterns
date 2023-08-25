using BaseProject.Models;
using Microsoft.EntityFrameworkCore;
using StrategyPattern.WebApp.Models;

namespace StrategyPattern.WebApp.Repositories
{
    public class ProductRepositoryFromSqlServer : IProductRepository
    {
        private readonly AppIdentityDbContext context;

        public ProductRepositoryFromSqlServer(AppIdentityDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetById(string id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await context.Products.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task Save(Product product)
        {
            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }
    }
}
