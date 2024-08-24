using FluentValidation;

namespace TicketApplication.Validators.BonValidators
{
    public class BonIdValidator : AbstractValidator<int>
    {
        public BonIdValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Bon ID must be greater than 0.");
        }
    }
}
