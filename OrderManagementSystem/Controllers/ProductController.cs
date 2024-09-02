using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderBase.Entities;
using OrderCore.DTO.Request;
using OrderCore.Interfaces;
using OrderManagementSystem.Responses;
using OrderManagementSystem.ViewModels;
using OrderManagementSystem.ViewModels.Request;
using OrderManagementSystem.ViewModels.Response;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductDTO> _validator;

        public ProductController(IProductService productService , IValidator<ProductDTO> productValidator) 
        { 
            _productService = productService;
            _validator = productValidator;
        }

        [HttpPost]
        public async Task<BaseResponse> AddProduct([FromBody]ProductVM addedProduct) {

            var product = addedProduct.Adapt<ProductDTO>();
            var results = _validator.Validate(product);

            if (results.IsValid)
            {
                var productDTO =  await _productService.AddProduct(product);
                var productResponseVM = productDTO.Adapt<ProductResponseVM>();

                if (productDTO != null)
                {
                    return new SuccessResponse<ProductResponseVM>
                    {
                        StatusCode = 200,
                        Message = "Product Added Successfully",
                        Data = productResponseVM
                    };
                }
                else
                {
                    return new BaseResponse
                    {
                        StatusCode = 400,
                        Message = "Can't Add Product",
                        
                    };
                }
            }
            else
            {
                return new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Product Data",
                    Errors = results.Errors

                };
            }
        }


        [HttpDelete]
        public async Task<BaseResponse> DeleteProduct(Guid id)
        {
            if (id != Guid.Empty)
            {
               var result = await _productService.DeleteProduct(id);
                if (result !=null)
                {
                    return new BaseResponse
                    {
                        StatusCode = 200,
                        Message = "Product Deleted Successfully"
                        
                    };
                }
                else {
                    return new BaseResponse
                    {
                        StatusCode = 404,
                        Message = "Can't Delete Product"

                    };
                }
            }
            else
            {
                return new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Product ID",
                };

            }
        }


        [HttpPut]
        public async Task<BaseResponse> UpdateProduct(Guid id, [FromBody]ProductVM updatedProduct)
        {
            var product = updatedProduct.Adapt<ProductDTO>();
            var results = _validator.Validate(product);

                if (results.IsValid)
                {
                    var result = await _productService.UpdateProduct(id, product);
                    if (result !=null )
                    {
                    var productResponseVM = result.Adapt<ProductResponseVM>();
                    return new SuccessResponse<ProductResponseVM>
                    {
                        StatusCode = 200,
                        Message = "Product Updated Successfully",
                        Data = productResponseVM

                    };
                    }
                    else 
                    {
                        return new BaseResponse
                        {
                            StatusCode = 400,
                            Message = "Failed To Update The Product",
                        };
                    }
                }
                else
                {
                    return new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Invalid Product Data",
                        Errors = results.Errors
                    };
                }
        }
           
        
        [HttpGet]
        public async Task<BaseResponse> GetProductById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _productService.GetProductById(id);
                if (product != null)
                {
                    var productResponseVM = product.Adapt<ProductResponseVM>();
                    return new SuccessResponse<ProductResponseVM>
                    {
                        StatusCode = 200,
                        Message = "Product Retrieved Successfully",
                        Data = productResponseVM
                    };
                }
                else {
                    return new BaseResponse
                    {
                        StatusCode = 404,
                        Message = "Product Not Found",
                    };

                }
            }
            else
            {
                return new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Product Not Found"
                };
            }

        }

        [HttpGet]
        public async Task<BaseResponse> GetAllInStockProducts()
        {

            List<Product> products = await _productService.GetAllInStockProducts();
            var productVM = products.Adapt<ProductVM>();

            if (!products.IsNullOrEmpty())
            {
                return new SuccessResponse<ProductVM>
                {
                    StatusCode = 200,
                    Data = productVM
                };
            }
            else
            {
                return new ErrorResponse
                {
                    StatusCode = 200,
                    Message = "No Products in Stock"
                }; 
            }
        }

        [HttpGet]
        public async Task<BaseResponse> GetAllProducts(Guid customerId)
        {
            if (customerId != Guid.Empty)
            {
                var products = await _productService.GetAllProducts(customerId);

                if (products != null)
                {
                    if (products.Count > 0)
                    {
                        var productResponseVM = products.Adapt<List<ProductResponseVM>>();
                        return new SuccessResponse<List<ProductResponseVM>>
                        {
                            StatusCode = 200,
                            Message = "Products Retrieved Successfully",
                            Data = productResponseVM
                        };
                    }
                    else {
                        return new BaseResponse
                        {
                            StatusCode = 200,
                            Message = "No Products Found To Retrieve"
                        };
                    }
                }
                else
                {
                    return new BaseResponse
                    {
                        StatusCode = 400,
                        Message = "Can't Retrieve Products"
                    };
                }
            }
            else {
                return new BaseResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Customer Id"
                };
            }
        }
        [HttpGet]
        public async Task<BaseResponse> GetProductsByType(string productType)
        {
            List<Product> products = await _productService.GetProductByType(productType);
            var productVM = products.Adapt<ProductVM>();

            if (!products.IsNullOrEmpty())
            {
                return new SuccessResponse<ProductVM>
                {
                    StatusCode = 200,
                    Data = productVM
                };
            }
            else
            {
                return new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Failed To Filter Products By Type"
                };
            }
        }
    }
}
