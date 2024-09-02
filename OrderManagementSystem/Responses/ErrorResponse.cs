using FluentValidation.Results;

namespace OrderManagementSystem.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public List<ValidationFailure> Errors { get; set; }

    }
}
