using Application.Features.Cards.DTOs;
using FluentValidation;

namespace Application.Features.Cards.Validators
{
    public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        public CreateCardRequestValidator()
        {
            RuleFor(x => x.DeckId).NotEmpty();

            RuleFor(x => x.Term)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Definition)
                .NotEmpty()
                .MaximumLength(512);

            RuleFor(x => x.Romanization)
                .MaximumLength(256)
                .When(x => x.Romanization is not null);
        }
    }
}
