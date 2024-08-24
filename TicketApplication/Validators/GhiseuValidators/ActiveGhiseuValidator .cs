using FluentValidation;
using TicketApplication.Services.Dtos;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class ActiveGhiseuValidator : AbstractValidator<GhiseuDto>
    {
        public ActiveGhiseuValidator()
        {
            RuleFor(dto => dto.Id).SetValidator(new GhiseuIdValidator());
            RuleFor(dto => dto.Activ).NotNull().WithMessage("The Activ status must be specified.");
        }
    }
}
