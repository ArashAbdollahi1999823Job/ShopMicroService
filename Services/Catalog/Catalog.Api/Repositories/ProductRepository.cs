using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Injecting and Counstructor

        private readonly ICatalogContext  _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        #endregion

        #region RestApi  
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(x => true).ToListAsync();
        }
        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x=>x.Name,name);

            return await _context.Products.Find(filter).ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCatagory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        #endregion

        #region CURD

        public async Task CreatedProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updataResult= await _context.Products.ReplaceOneAsync(filter:x=>x.Id==product.Id,replacement:product );

            return updataResult.IsAcknowledged && updataResult.ModifiedCount> 0;
        }


        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id,id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount>0;
        }
        #endregion

    }
}
