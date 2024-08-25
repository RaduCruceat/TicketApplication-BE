using FluentValidation;
using TicketApplication.Services.Dtos;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class AddGhiseuValidator : AbstractValidator<GhiseuDto>
    {
        public AddGhiseuValidator()
        {
            RuleFor(g => g.Cod).NotEmpty().MaximumLength(50);
            RuleFor(g => g.Denumire).NotEmpty().MaximumLength(50);
            RuleFor(g => g.Descriere).MaximumLength(500);
            RuleFor(g => g.Icon).MaximumLength(200);
            RuleFor(g => g.Activ).NotNull();
        }
    }
}
