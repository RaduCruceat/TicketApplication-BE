using FluentValidation;

namespace TicketApplication.Validators.BonValidators
{
    public class UpdateBonStatusValidator : AbstractValidator<int>
    {
        public UpdateBonStatusValidator()
        {
            RuleFor(id => id).SetValidator(new BonIdValidator());
        }
    }
}
