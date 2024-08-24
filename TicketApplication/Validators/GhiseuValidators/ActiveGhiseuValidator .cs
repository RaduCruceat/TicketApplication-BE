using FluentValidation;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class ActiveGhiseuValidator : AbstractValidator<int>
    {
        public ActiveGhiseuValidator()
        {
            RuleFor(id => id).SetValidator(new GhiseuIdValidator());
        }
    }
}
