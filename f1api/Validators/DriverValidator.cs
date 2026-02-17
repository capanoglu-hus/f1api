using f1api.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace f1api.Validators
{
    public class DriverValidator :AbstractValidator<CreateDriverRequest>
    {
       public DriverValidator() 
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("Driver name is required.")
                .MaximumLength(100).WithMessage("Driver name must not exceed 100 characters.");

            RuleFor(s => s.RacingNumber)
                .NotNull().WithMessage("Racing number is required.")
                .InclusiveBetween(1, 99).WithMessage("Racing number must be between 1 and 99.");

        }
    }
}
