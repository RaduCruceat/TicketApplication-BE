using FluentValidation;
using TicketApplication.Services.Dtos;

namespace TicketApplication.Validators.BonValidators
{
    public class UpdateBonStatusValidator : AbstractValidator<BonDto>
    {
        public UpdateBonStatusValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new BonIdValidator());
            RuleFor(dto => dto.Stare).IsInEnum();
        }
    }
}
