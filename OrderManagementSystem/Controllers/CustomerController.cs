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
        public async Task<BaseResponse> Register([FromBody]CustomerRequestVM addedCustomer) 
        {
            var customer = addedCustomer.Adapt<CustomerRequestDTO>();
            var results = await _customerValidator.ValidateAsync(customer);

                if (results.IsValid)
                {
                        var customerDTO = await _customerService.Register(customer);
                        var customerResponseVM = customerDTO.Adapt<CustomerResponseVM>();

                        if (customerResponseVM != null)
                        {
                            return new SuccessResponse<CustomerResponseVM>
                            {
                                StatusCode = 200,
                                Message = ("Customer Added Successfully"),
                                Data = customerResponseVM
                            };
                        }
                        else
                        {
                            return new BaseResponse
                            {
                                StatusCode = 400,
                                Message = ("Cannot use an already registered email"),
                            };
                        }
                } 
                else 
                {
                    return new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = ("Invalid Customer Data"),
                        Errors = results.Errors
                    };
                }
        }


        [HttpPost]
        public async Task<BaseResponse> Login(string email, string password)
        {
            var customerDTO = await _customerService.ValidateEmail(email);
            var customerRequestDTO = customerDTO.Adapt<CustomerRequestDTO>();
            var customerResponseVM = customerDTO.Adapt<CustomerResponseVM>();

            if (customerDTO != null)
            {
                var customerAuthentication = _customerService.ValidatePassword(password,customerRequestDTO.PasswordHash, customerRequestDTO.PasswordSalt);

                if (customerAuthentication == true)
                {
                    return new SuccessResponse<CustomerResponseVM>
                    {
                        StatusCode = 200,
                        Message = ("Logged In Successfully"),
                        Data = customerResponseVM
                    };
                }
                else
                {
                    return new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = ("Incorrect Password"),
                        Errors = { }
                    };
                }

            }
            else {
                return new ErrorResponse
                {
                    StatusCode = 404,
                    Message = ("No Existing Account With This Email"),
                    Errors = { }
                };

            }

        }

    }
}
