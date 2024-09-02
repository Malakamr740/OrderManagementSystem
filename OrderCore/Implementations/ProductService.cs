using Mapster;
using OrderBase.Entities;
using OrderBase.Interfaces;
using OrderCore.DTO.Request;
using OrderCore.DTO.Response;
using OrderCore.Interfaces;

namespace OrderCore.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork , ICustomerRepository customerRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponseDTO> AddProduct(ProductDTO addedProduct)       
        {
            Product product = addedProduct.Adapt<Product>();
            Product productData = await _productRepository.GetProductByName(addedProduct.Name);

            if (productData == null)
            {
                product.IsDeleted = false;
                await _productRepository.AddProduct(product);
                var productResponseDTO = product.Adapt<ProductResponseDTO>();
                var isSaved = await _unitOfWork.SaveChangesAsync();
                var result = isSaved == 1 ? productResponseDTO : null; 
                return result;
            }
            else {
                return null;
            }
        }

        public async Task<ProductDTO> DeleteProduct(Guid id)
        {
            Product product = await _productRepository.GetProductById(id);
            ProductDTO productDTO = product.Adapt<ProductDTO>();

            if (productDTO != null)
            {
                product.IsDeleted = !(product.IsDeleted);
                _productRepository.DeleteProduct(product);
                var isSaved = await _unitOfWork.SaveChangesAsync();
                var result = isSaved == 1 ? productDTO : null;
                return result;
            }
            else {
                return null;
            }

        }

        public async Task<List<Product>> GetAllInStockProducts()
        {
            return await _productRepository.GetAllProductsInStock();
        }

        public async Task<List<ProductResponseDTO>> GetAllProducts(Guid customerId)
        {
            Customer customer = await _customerRepository.GetCustomerById(customerId);
            
            if (customer.IsAdmin == true)
            {
                var result = await _productRepository.GetAllProducts();
                var productResponseDTO = result.Adapt<List<ProductResponseDTO>>();
                return productResponseDTO;
            }
            else 
            {
                bool isDeleted = false;
                var result = await _productRepository.GetAllProductsFilteredByIsDeleted(isDeleted);
                var productResponseDTO = result.Adapt<List<ProductResponseDTO>>();
                return productResponseDTO;
            }
        }

        public async Task<ProductResponseDTO> GetProductById(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            var productResponseDTO = product.Adapt<ProductResponseDTO>();
            return productResponseDTO;
        }

        public Task<Product> GetProductByName(string productName)
        { 
            return _productRepository.GetProductByName(productName);    
        }

        public async Task<List<Product>> GetProductByType(string productType)
        {
            return await _productRepository.GetProductsByType(productType);
        }

        public async Task<ProductResponseDTO> UpdateProduct(Guid id, ProductDTO updatedProduct)
        {
            var storedProduct = await _productRepository.GetProductById(id);
            var product = updatedProduct.Adapt<Product>();
            _productRepository.UpdateProduct(product, storedProduct);
            var productResponseDTO = storedProduct.Adapt<ProductResponseDTO>();
            var isSaved = Convert.ToBoolean(await _unitOfWork.SaveChangesAsync());
            var result = isSaved == true ? productResponseDTO : null;
            return result;
        }
    }
}
