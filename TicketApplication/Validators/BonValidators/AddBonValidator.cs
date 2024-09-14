using FluentValidation;
using TicketApplication.Services.Dtos.BonDtos;
using TicketApplication.Validators.GhiseuValidators;

namespace TicketApplication.Validators.BonValidators
{
    public class AddBonValidator : AbstractValidator<BonDto>
    {
        public AddBonValidator()
        {
            RuleFor(b => b.IdGhiseu).SetValidator(new GhiseuIdValidator());
            RuleFor(b => b.Stare).IsInEnum().WithMessage("Starea trebuie sa aiba valoarea 0,1 sau 2.");
        }
    }
}
