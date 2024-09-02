using FluentValidation;
using OrderCore.DTO.Request;

namespace RunTime.Validations
{
    public class CustomerValidator : AbstractValidator<CustomerRequestDTO>
    {
        public CustomerValidator() {

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Email is Required").EmailAddress().WithMessage("Invalid Email Address");
                                                                 

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name is Required").MaximumLength(50)
                                                          .WithMessage($"You reached Maximum Characters Allowed (50 characters)");

            RuleFor(x => x.Phone).Cascade(CascadeMode.Stop).NotEmpty().Must(ph => ph.Length == 14)
                                                           .WithMessage($"Phone Number Must be 13 Numbers Including Country Code Ex: (+02)");


            RuleFor(x => x.Address).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Address is Required");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("Password is required")
               .MinimumLength(8)
               .WithMessage("Password must be at least 8 characters long")
               .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\d]).+$")
               .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");

            RuleFor(x => x.Status).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Status is Required")
                                                            .Must(status=> status.ToLower() == "active" || status.ToLower() == "inactive").WithMessage("Status must be Active or Inactive");

           


        }
    }
}
