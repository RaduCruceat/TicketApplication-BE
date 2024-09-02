using FluentValidation;
using TicketApplication.Services.Dtos.GhiseuDtos;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class EditGhiseuValidator : AbstractValidator<(int, EditGhiseuDto)>
    {
        public EditGhiseuValidator()
        {
            RuleFor(tuple => tuple.Item1).GreaterThan(0);
            RuleFor(tuple => tuple.Item2.Cod).NotEmpty().MaximumLength(50);
            RuleFor(tuple => tuple.Item2.Denumire).NotEmpty().MaximumLength(50);
            RuleFor(tuple => tuple.Item2.Descriere).MaximumLength(500);
            RuleFor(tuple => tuple.Item2.Icon).MaximumLength(200);
        }
    }
}
