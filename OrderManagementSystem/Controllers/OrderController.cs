using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using OrderCore.DTO.Request;
using OrderCore.Interfaces;
using OrderManagementSystem.Responses;
using OrderManagementSystem.ViewModels.Request;
using OrderManagementSystem.ViewModels.Response;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderRequestDTO> _orderValidator;
        public OrderController(IOrderService orderService , IValidator<OrderRequestDTO> orderValidator )
        {
            _orderService = orderService;
            _orderValidator = orderValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]OrderRequestVM addedOrder)
        {
            var order = addedOrder.Adapt<OrderRequestDTO>();
            var results = _orderValidator.Validate(order);

            if (results.IsValid)
            {
                var result = await _orderService.AddOrder(order);
                var orderResponseVM = result.Adapt<OrderResponseVM>();

                if (result != null)
                {
                    SuccessResponse<OrderResponseVM> successResponse = new SuccessResponse<OrderResponseVM>() {
                        StatusCode = 200,
                        Message = "Order Added Successfully",
                        Data = orderResponseVM
                    };

                    return Ok(successResponse);
                }
                else 
                {
                    BaseResponse baseResponse = new BaseResponse() {
                        StatusCode = 400,
                        Message = "Can't Add Order"
                    };
                    return BadRequest(baseResponse);
                }
            }
            else 
            {
                ErrorResponse errorResponse = new ErrorResponse()
                {
                    StatusCode = 400,
                    Message = "Invalid Order Data",
                    Errors = results.Errors
                };
                return BadRequest(errorResponse);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrdersByCustomerId(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _orderService.GetOrdersByCustomerId(id);
                var orderVM = result.Adapt<OrderRequestVM>();

                if (result != null)
                {
                    SuccessResponse<OrderRequestVM> successResponse = new SuccessResponse<OrderRequestVM>() {
                        StatusCode = 200,
                        Message = "Orders Retrieved Successfully",
                        Data = orderVM
                    };

                    return Ok(successResponse);
                }
                else 
                {
                    BaseResponse baseResponse = new BaseResponse() {
                        StatusCode = 400,
                        Message = "Invalid Id"
                    };
                    return BadRequest(baseResponse);
                }
            }
            else
            {
                BaseResponse baseResponse = new BaseResponse() {
                    StatusCode = 400,
                    Message = "Can't Retrieve Orders"
                };
                return BadRequest(baseResponse);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            if (id != Guid.Empty)
            {

                var result = await _orderService.DeleteOrder(id);

                if (result == true)
                {
                    SuccessResponse<OrderRequestVM> successResponse = new SuccessResponse<OrderRequestVM>() {
                        StatusCode = 200,
                        Message = "Order Deleted successfully"

                    };
                    return Ok(successResponse);
                }
                else
                {
                    BaseResponse baseResponse = new BaseResponse() {
                    
                        StatusCode = 400,
                        Message = "Failed To Delete Order",
                    };
                    return BadRequest(baseResponse);
                }
            }
            else {
                BaseResponse baseResponse = new BaseResponse() {
                
                    StatusCode = 404,
                    Message = "Invalid Order Id",
                };
                return NotFound(baseResponse);
            }
            
        }

    }
}