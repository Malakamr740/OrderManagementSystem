
using FluentValidation.Results;

namespace OrderManagementSystem.Responses
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
