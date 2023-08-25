using StrategyPattern.WebApp.Models;

namespace StrategyPattern.WebApp.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);

        Task<List<Product>> GetAllByUserId(string userId);

        Task Save(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
