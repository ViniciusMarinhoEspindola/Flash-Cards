using Application.Features.Users.DTOs;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Users.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator(IUser userRepository)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, ct) => !await userRepository.ExistsByEmailAsync(email, ct))
                .WithMessage("E-mail já cadastrado.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
