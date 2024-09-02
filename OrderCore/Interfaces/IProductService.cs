using OrderBase.Entities;
using OrderCore.Implementations;
using OrderCore.DTO.Response;
using OrderCore.DTO.Request;
namespace OrderCore.Interfaces
{
    public interface IProductService
    {
        public Task<ProductResponseDTO> AddProduct(ProductDTO productToAdd);
        public Task<ProductResponseDTO> GetProductById(Guid id);
        public Task<ProductResponseDTO> UpdateProduct(Guid id, ProductDTO updatedProductData);
        public Task<ProductDTO> DeleteProduct(Guid id);
        public Task<List<Product>> GetAllInStockProducts();
        public Task<List<ProductResponseDTO>> GetAllProducts(Guid customerId);
        public Task<List<Product>> GetProductByType(string productType);
        public Task<Product> GetProductByName(string productName);
    }
}
