using OrderBase.Entities;

namespace OrderBase.Interfaces 
{
   public interface IProductRepository
    {
        public Task AddProduct(Product productToAdd);
        public void DeleteProduct(Product product);
        public void UpdateProduct(Product productToUpdate, Product product);
        public Task<Product> GetProductById(Guid id);
        public Task<List<Product>> GetProductsByType(string productType);
        public Task<Product> GetProductByName(string productName);
        public Task<List<Product>> GetAllProductsInStock();
        public Task<List<Product>> GetAllProducts();
        public Task<List<Product>> GetAllProductsFilteredByIsDeleted(bool isDeleted);


    }
}
