using FluentValidation;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class GhiseuIdValidator : AbstractValidator<int>
    {
        public GhiseuIdValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("ID-ul ghiseului trebuie sa fie mai mare ca 0.");
        }
    }
}
