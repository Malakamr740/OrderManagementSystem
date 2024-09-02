using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderBase.Entities;
using OrderCore.DTO.Request;
using OrderCore.Interfaces;
using OrderManagementSystem.Responses;
using OrderManagementSystem.ViewModels;
using OrderManagementSystem.ViewModels.Request;
using System.Collections.Generic;

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
        public async Task<IActionResult> AddProduct([FromBody]ProductVM addedProduct) {

            var product = addedProduct.Adapt<ProductDTO>();
            var results = _validator.Validate(product);

            if (results.IsValid)
            {
                var productDTO =  await _productService.AddProduct(product);
                var productResponseVM = productDTO.Adapt<ProductResponseVM>();

                if (productDTO != null)
                {
                    SuccessResponse<ProductResponseVM> successResponse = new SuccessResponse<ProductResponseVM>() {
                        StatusCode = 200,
                        Message = "Product Added Successfully",
                        Data = productResponseVM
                    };
                    return Ok(successResponse);
                }
                else
                {
                    BaseResponse baseResponse =  new BaseResponse()
                    {
                        StatusCode = 400,
                        Message = "Can't Add Product",
                    };
                    return BadRequest(baseResponse);
                }
            }
            else
            {
                 ErrorResponse errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = "Invalid Product Data",
                    Errors = results.Errors
                };
                return BadRequest(errorResponse);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id != Guid.Empty)
            {
               var result = await _productService.DeleteProduct(id);
                if (result !=null)
                {
                    BaseResponse baseResponse = new BaseResponse()
                    {
                        StatusCode = 200,
                        Message = "Product Deleted Successfully"
                    };
                    return Ok(baseResponse);
                }
                else {
                    BaseResponse baseResponse = new BaseResponse()
                    {
                        StatusCode = 404,
                        Message = "Can't Delete Product"
                    };
                    return NotFound(baseResponse);
                }
            }
            else
            {
                BaseResponse baseResponse =  new BaseResponse()
                {
                    StatusCode = 400,
                    Message = "Invalid Product ID",
                };
                return BadRequest(baseResponse);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]ProductVM updatedProduct)
        {
            var product = updatedProduct.Adapt<ProductDTO>();
            var results = _validator.Validate(product);

                if (results.IsValid)
                {
                    var result = await _productService.UpdateProduct(id, product);
                    if (result !=null )
                    {
                    var productResponseVM = result.Adapt<ProductResponseVM>();
                    SuccessResponse<ProductResponseVM> successResponse = new SuccessResponse<ProductResponseVM>
                    {
                        StatusCode = 200,
                        Message = "Product Updated Successfully",
                        Data = productResponseVM
                    };
                    return Ok(successResponse);
                    }
                    else 
                    {
                        BaseResponse baseResponse =  new BaseResponse()
                        {
                            StatusCode = 400,
                            Message = "Failed To Update The Product",
                        };
                        return BadRequest(baseResponse);
                    }
                }
                else
                {
                    ErrorResponse errorResponse =  new ErrorResponse()
                    {
                        StatusCode = 400,
                        Message = "Invalid Product Data",
                        Errors = results.Errors
                    };
                return BadRequest(errorResponse);
                }
        }
           
        
        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _productService.GetProductById(id);
                if (product != null)
                {
                    var productResponseVM = product.Adapt<ProductResponseVM>();
                    SuccessResponse < ProductResponseVM > successResponse  = new SuccessResponse<ProductResponseVM>()
                    {
                        StatusCode = 200,
                        Message = "Product Retrieved Successfully",
                        Data = productResponseVM
                    };
                    return Ok(successResponse);
                }
                else {
                     BaseResponse baseResponse =  new BaseResponse()
                    {
                        StatusCode = 404,
                        Message = "Product Not Found"
                    };
                    return NotFound(baseResponse);
                }
            }
            else
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Product Not Found"
                };
                return NotFound(errorResponse);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllInStockProducts()
        {

            List<Product> products = await _productService.GetAllInStockProducts();
            var productVM = products.Adapt<ProductVM>();

            if (!products.IsNullOrEmpty())
            {
                SuccessResponse<ProductVM> successResponse = new SuccessResponse<ProductVM>()
                {
                    StatusCode = 200,
                    Data = productVM
                };
                return Ok(successResponse);
            }
            else
            {
                BaseResponse baseResponse = new BaseResponse()
                {
                    StatusCode = 404,
                    Message = "No Products in Stock"
                };
                return NotFound(baseResponse);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(Guid customerId)
        {
            if (customerId != Guid.Empty)
            {
                var products = await _productService.GetAllProducts(customerId);

                if (products != null)
                {
                    if (products.Count > 0)
                    {
                        var productResponseVM = products.Adapt<List<ProductResponseVM>>();
                        SuccessResponse <List<ProductResponseVM>> successResponse = new SuccessResponse<List<ProductResponseVM>>
                        {
                            StatusCode = 200,
                            Message = "Products Retrieved Successfully",
                            Data = productResponseVM
                        };
                        return Ok(productResponseVM);
                    }
                    else {
                        BaseResponse baseResponse = new BaseResponse()
                        {
                            StatusCode = 404,
                            Message = "No Products Found To Retrieve"
                        };
                        return NotFound(baseResponse);
                    }
                }
                else
                {
                    BaseResponse baseResponse = new BaseResponse()
                    {
                        StatusCode = 400,
                        Message = "Can't Retrieve Products"
                    };
                    return NotFound(baseResponse);
                }
            }
            else {
                BaseResponse baseResponse = new BaseResponse()
                {
                    StatusCode = 400,
                    Message = "Invalid Customer Id"
                };
                return BadRequest(baseResponse);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByType(string productType)
        {
            List<Product> products = await _productService.GetProductByType(productType);
            var productVM = products.Adapt<ProductVM>();

            if (!products.IsNullOrEmpty())
            {
                SuccessResponse < ProductVM > successResponse =  new SuccessResponse<ProductVM>()
                {
                    StatusCode = 200,
                    Data = productVM
                };
                return Ok(successResponse);
            }
            else
            {
                BaseResponse baseRespone =  new BaseResponse()
                {
                    StatusCode = 400,
                    Message = "Failed To Filter Products By Type"
                };
                return BadRequest(baseRespone);
            }
        }
    }
}
