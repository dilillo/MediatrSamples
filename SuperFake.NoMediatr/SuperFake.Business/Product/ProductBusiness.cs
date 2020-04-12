using Microsoft.EntityFrameworkCore;
using SuperFake.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperFake.Business
{
    public class ProductBusiness
    {
        private readonly SuperFakeDbContext _dbContext;

        public ProductBusiness(SuperFakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> GetAllProducts()
        {
            return _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductDetails(int productID)
        {
            return await _dbContext.Products.FindAsync(productID);
        }

        public Task<bool> ProductExists(int productID)
        {
            return _dbContext.Products.AnyAsync(e => e.ID == productID);
        }

        public async Task CreateProduct(Product product)
        {
            await VerifyProductNameIsUnique(product);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            await VerifyProductExists(product.ID);

            await VerifyProductNameIsUnique(product);

            _dbContext.Update(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productID)
        {
            await VerifyProductExists(productID);

            await VerifyProductHasNoOrders(productID);

            var product = await _dbContext.Products.FindAsync(productID);

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();
        }

        private async Task VerifyProductHasNoOrders(int productID)
        {
            var productHasOrders = await _dbContext.OrderItems.AnyAsync(i => i.ProductID == productID);

            if (productHasOrders)
                throw new ProductWithOrdersCannotBeDeletedException();
        }

        private async Task VerifyProductNameIsUnique(Product product)
        {
            var nameExists = await _dbContext.Products.AnyAsync(i => i.ID != product.ID && i.Name == product.Name);

            if (nameExists)
                throw new ProductNameMustBeUniqueException();
        }

        private async Task VerifyProductExists(int productID)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.ID == productID);

            if (!productExists)
                throw new ProductDoesNotExistException();
        }
    }
}
