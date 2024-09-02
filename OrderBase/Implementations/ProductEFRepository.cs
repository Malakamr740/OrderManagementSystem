using Microsoft.EntityFrameworkCore;
using OrderBase.DBContext;
using OrderBase.Entities;
using OrderBase.Interfaces;

namespace OrderBase.Implementations
{
    public class ProductEFRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductEFRepository(Context context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
            product.CreatedOn = DateTime.UtcNow;
            await _context.Products.AddAsync(product);
        }

        public void DeleteProduct(Product product)
        {
            product.UpdatedOn = DateTime.UtcNow;
            _context.Products.Update(product);
            
        }

        public async Task<List<Product>> GetAllProductsInStock()
        {
            return await _context.Products.Where(p => p.Quantity > 0).ToListAsync();
        }


        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsFilteredByIsDeleted(bool isDeleted)
        {
            return await _context.Products.Where(product => product.IsDeleted == isDeleted).ToListAsync();
        }


        public async Task<Product> GetProductById(Guid id)
        {
            Product product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> GetProductByName(string productName)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(product => product.Name == productName);
            return product;
        }

        public async Task<List<Product>> GetProductsByType(string productType)
        {
                List<Product> products = await _context.Products.Where(product => product.Type == productType).ToListAsync();
                return products;   
        }

        public void UpdateProduct (Product updatedProduct, Product product)
        {
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Amount = updatedProduct.Amount;
            product.Type = updatedProduct.Type;
            product.Status = product.Status;
            product.UpdatedOn = DateTime.UtcNow;
            _context.Products.Update(product);
        }
    }
}