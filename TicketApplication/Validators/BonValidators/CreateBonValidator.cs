using FluentValidation;
using TicketApplication.Services.Dtos;
using TicketApplication.Validators.GhiseuValidators;

namespace TicketApplication.Validators.BonValidators
{
    public class CreateBonValidator : AbstractValidator<BonDto>
    {
        public CreateBonValidator()
        {
            RuleFor(b => b.IdGhiseu).SetValidator(new GhiseuIdValidator());
            RuleFor(b => b.Stare).IsInEnum();
        }
    }
}
