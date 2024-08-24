using FluentValidation;
using TicketApplication.Services.Dtos;

namespace TicketApplication.Validators.GhiseuValidators
{
    public class DeleteGhiseuValidator : AbstractValidator<int>
    {
        public DeleteGhiseuValidator()
        {
            RuleFor(id => id).SetValidator(new GhiseuIdValidator());
        }
    }
}
