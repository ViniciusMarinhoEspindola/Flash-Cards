using Application.Common;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Users.Services
{
    public class UserService(IUser _userRepository, IValidator<RegisterRequestDto> _registerValidator)
    {
        public async Task<Result<User>> GetByIdAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByIdAsync(userId, ct);

            if (user is null)
                return Result<User>.Fail(AppError.NotFound("Usuário não encontrado."));

            return Result<User>.Ok(user);
        }

        public async Task<Result<User>> CreateAsync(RegisterRequestDto dto, CancellationToken ct = default)
        {
            var validation = await _registerValidator.ValidateAsync(dto, ct);
            if (!validation.IsValid) return validation.ToFailResult<User>();

            var user = User.Create(dto.Email, dto.Email, dto.Password);
            await _userRepository.AddAsync(user, ct);

            return Result<User>.Ok(user);
        }
    }
}
