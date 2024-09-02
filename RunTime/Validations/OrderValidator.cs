using FluentValidation;
using OrderCore.DTO.Request;

namespace RunTime.Validations
{
    public class OrderValidator : AbstractValidator<OrderRequestDTO>
    {
        public OrderValidator()
        {
           
        }
    }
}
