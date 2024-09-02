using Microsoft.AspNetCore.Mvc;
using Mapster;
using OrderCore.Interfaces;
using OrderManagementSystem.Responses;
using FluentValidation;
using OrderCore.DTO.Request;
using OrderManagementSystem.ViewModels.Request;
using OrderManagementSystem.ViewModels.Response;
namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerRequestDTO> _customerValidator;
        public CustomerController(ICustomerService customerService , IValidator<CustomerRequestDTO> customerValidator) 
        { 
            _customerService = customerService;
            _customerValidator = customerValidator;
        } 


        [HttpPost]
        public async Task<IActionResult> Register([FromBody]CustomerRequestVM addedCustomer) 
        {
            var customer = addedCustomer.Adapt<CustomerRequestDTO>();
            var results = await _customerValidator.ValidateAsync(customer);

                if (results.IsValid)
                {
                    var customerDTO = await _customerService.Register(customer);
                    var customerResponseVM = customerDTO.Adapt<CustomerResponseVM>();

                    if (customerResponseVM != null)
                    {
                        SuccessResponse<CustomerResponseVM> successResponse = new SuccessResponse<CustomerResponseVM>()
                        {
                            StatusCode = 200,
                            Message = ("Customer Added Successfully"),
                            Data = customerResponseVM
                        };
                           return Ok(successResponse);
                    }
                    else
                    {
                        BaseResponse errorResponse = new BaseResponse()
                        {
                            StatusCode = 400,
                            Message = ("Cannot use an already registered email")
                        };
                        return BadRequest(errorResponse);
                    }
                } 
                else 
                {
                    ErrorResponse errorResponse = new ErrorResponse() {
                        StatusCode = 400,
                        Message = ("Invalid Customer Data"),
                        Errors = results.Errors
                    };
                    return BadRequest(errorResponse);

            }
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var customerDTO = await _customerService.ValidateEmail(email);
            var customerRequestDTO = customerDTO.Adapt<CustomerRequestDTO>();
            var customerResponseVM = customerDTO.Adapt<CustomerResponseVM>();

            if (customerDTO != null)
            {
                var customerAuthentication = _customerService.ValidatePassword(password,customerRequestDTO.PasswordHash, customerRequestDTO.PasswordSalt);

                if (customerAuthentication == true)
                {
                    SuccessResponse < CustomerResponseVM > successResponse = new SuccessResponse<CustomerResponseVM>()
                    {
                        StatusCode = 200,
                        Message = ("Logged In Successfully"),
                        Data = customerResponseVM
                    };


                    return Ok(successResponse);
                }
                else
                {
                    BaseResponse errorResponse = new BaseResponse()
                    {
                        StatusCode = 400,
                        Message = ("Incorrect Password"),
                    };
                    return  BadRequest(errorResponse);
                }
            }
            else {
                BaseResponse errorResponse = new BaseResponse() {
                    StatusCode = 404,
                    Message = ("No Existing Account With This Email"),
                };
                    return NotFound(errorResponse);

            }

        }

    }
}
