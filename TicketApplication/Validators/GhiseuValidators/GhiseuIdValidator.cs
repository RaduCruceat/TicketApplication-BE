using FluentValidation;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class GhiseuIdValidator : AbstractValidator<int>
    {
        public GhiseuIdValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Ghiseu ID must be greater than 0.");
        }
    }
}
