using FluentValidation;

namespace TicketApplication.Validators.BonValidators
{
    public class BonIdValidator : AbstractValidator<int>
    {
        public BonIdValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("ID-ul bonului trebuie sa fie mai mare ca 0.");
        }
    }
}
