using FluentValidation;
using OrderCore.DTO.Request;

namespace RunTime.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name Cannot Be Empty").MaximumLength(50).WithMessage("Maximum length is 50 Characters");
            RuleFor(x => x.Type).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Define the product's Type");
            RuleFor(x => x.Amount).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Product Price Is Required").LessThanOrEqualTo(99999999.99).GreaterThanOrEqualTo(0.01)
                                                                       .WithMessage("Price must be between 0.01 and 99999999.99");
            RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Product's Description is required")
                                                                 .MaximumLength(150).WithMessage("Maximum Character Length is 150 Characters");
            RuleFor(x => x.Status).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Status is Required").Must(status => status.ToLower() == "active" || status.ToLower() == "inactive")
                                                                       .WithMessage("Status must be Active or Inactive");
        }
    }
}
