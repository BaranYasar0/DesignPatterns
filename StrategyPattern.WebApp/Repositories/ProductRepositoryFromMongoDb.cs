using MongoDB.Driver;
using StrategyPattern.WebApp.Models;

namespace StrategyPattern.WebApp.Repositories
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        private readonly IMongoCollection<Product> productCollection;

        public ProductRepositoryFromMongoDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("ProductDb");

            this.productCollection = database.GetCollection<Product>("Products");
        }

        public async Task<Product> GetById(string id)
        {
            return await productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await productCollection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task Save(Product product)
        {
            await productCollection.InsertOneAsync(product);
        }

        public async Task Update(Product product)
        {
            await productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);
        }

        public async Task Delete(Product product)
        {
            await productCollection.DeleteOneAsync(x => x.Id == product.Id);
        }
    }
}
