using FluentValidation;
using TicketApplication.Services.Dtos.BonDtos;

namespace TicketApplication.Validators.BonValidators
{
    public class EditBonStatusValidator : AbstractValidator<BonDto>
    {
        public EditBonStatusValidator()
        {
            RuleFor(dto => dto.Stare).IsInEnum().WithMessage("Invalid value for Stare.");
        }
    }
}
