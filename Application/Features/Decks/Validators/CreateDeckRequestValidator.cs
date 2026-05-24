using Application.Features.Decks.DTOs;
using FluentValidation;

namespace Application.Features.Decks.Validators
{
    public class CreateDeckRequestValidator : AbstractValidator<CreateDeckRequest>
    {
        public CreateDeckRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
