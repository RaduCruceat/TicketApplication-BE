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
            RuleFor(b => b.Stare).IsInEnum().WithMessage("Invalid value for Stare.");
        }
    }
}
