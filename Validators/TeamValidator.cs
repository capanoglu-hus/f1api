using f1api.Dtos;
using FluentValidation;

namespace f1api.Validators
{
    public class TeamValidator : AbstractValidator<CreateTeamRequest>
    {
        public TeamValidator() 
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Team name is have to")
                .MaximumLength(100).WithMessage("Team name must not exceed 100 characters.");

            RuleFor(t => t.Principal)
                .NotNull().WithMessage("Team principal is required.");
        }

    }
}
