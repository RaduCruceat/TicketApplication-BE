using FluentValidation;
using TicketApplication.Services.Dtos.GhiseuDtos;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class ActiveGhiseuValidator : AbstractValidator<GhiseuDto>
    {
        public ActiveGhiseuValidator()
        {
            RuleFor(dto => dto.Activ).NotNull().WithMessage("The Activ status must be specified.");
        }
    }
}
