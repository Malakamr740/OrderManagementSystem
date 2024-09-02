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
        public async Task<BaseResponse> AddOrder([FromBody]OrderRequestVM addedOrder)
        {
            var order = addedOrder.Adapt<OrderRequestDTO>();
            var results = _orderValidator.Validate(order);

            if (results.IsValid)
            {
                var result = await _orderService.AddOrder(order);
                var orderResponseVM = result.Adapt<OrderResponseVM>();

                if (result != null)
                {
                    return new SuccessResponse<OrderResponseVM>
                    {
                        StatusCode = 200,
                        Message = "Order Added Successfully",
                        Data = orderResponseVM
                    };
                }
                else 
                {
                    return new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Can't Add Order",
                    };
                }
            }
            else 
            {
                return new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Order Data",
                    Errors = results.Errors
                };
            }
        }


        [HttpGet]
        public async Task<BaseResponse> GetAllOrdersByCustomerId(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _orderService.GetOrdersByCustomerId(id);
                var orderVM = result.Adapt<OrderRequestVM>();

                if (result != null)
                {
                    return new SuccessResponse<OrderRequestVM>
                    {
                        StatusCode = 200,
                        Message = "Orders Retrieved Successfully",
                        Data = orderVM
                    };
                }
                else 
                {
                    return new ErrorResponse
                    {
                        StatusCode = 200,
                        Message = "Invalid Id"

                    };
                }
            }
            else
            { 
                return new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Can't Retrieve Orders"
                };
            }
        }


        [HttpDelete]
        public async Task<BaseResponse> DeleteOrder(Guid id)
        {
            if (id != Guid.Empty)
            {

                var result = await _orderService.DeleteOrder(id);

                if (result == true)
                {
                    return new SuccessResponse<OrderRequestVM>
                    {
                        StatusCode = 200,
                        Message = "Order Deleted successfully",
                    };
                }
                else
                {
                    return new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Failed To Delete Order",
                    };
                }
            }
            else {
                return new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Invalid Order Id",
                };
            }
            
        }

    }
}