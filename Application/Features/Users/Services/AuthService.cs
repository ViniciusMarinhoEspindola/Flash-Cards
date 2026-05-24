using Application.Common;
using Application.Contracts.Common;
using Application.Features.Users.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Users.Services
{
    public class AuthService(
        IUser _user,
        IPasswordHasher _hasher,
        ITokenService _tokens,
        IValidator<RegisterRequestDto> _registerValidator,
        IValidator<LoginRequest> _loginValidator)
    {
        public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequestDto request, CancellationToken ct = default)
        {
            var validation = await _registerValidator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return validation.ToFailResult<AuthResponse>();

            var passwordHash = _hasher.Hash(request.Password);
            var name = !string.IsNullOrWhiteSpace(request.Name) ? request.Name : request.Email;
            var user = User.Create(request.Email, name, passwordHash);

            var refreshToken = _tokens.GenerateRefreshToken();
            user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

            await _user.AddAsync(user, ct);

            return Result<AuthResponse>.Ok(new AuthResponse(
                _tokens.GenerateAccessToken(user),
                refreshToken,
                user.RefreshTokenExpiresAt!.Value,
                user.Id,
                user.Email,
                user.Name
            ));
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var validation = await _loginValidator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return validation.ToFailResult<AuthResponse>();

            var user = await _user.GetByEmailAsync(request.Email, ct);

            if (user is null || !_hasher.Verify(request.Password, user.PasswordHash))
                return Result<AuthResponse>.Fail(AppError.Unauthorized("E-mail ou senha inválidos."));

            var refreshToken = _tokens.GenerateRefreshToken();
            user.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));
            await _user.UpdateAsync(user, ct);

            return Result<AuthResponse>.Ok(new AuthResponse(
                _tokens.GenerateAccessToken(user),
                refreshToken,
                user.RefreshTokenExpiresAt!.Value,
                user.Id,
                user.Email,
                user.Name
            ));
        }

        public async Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default)
        {
            var user = await _user.GetByRefreshTokenAsync(request.RefreshToken, ct);

            if (user is null || user.RefreshTokenExpiresAt < DateTime.UtcNow)
                return Result<AuthResponse>.Fail(AppError.Unauthorized("Refresh token inválido ou expirado."));

            var newRefreshToken = _tokens.GenerateRefreshToken();
            user.SetRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
            await _user.UpdateAsync(user, ct);

            return Result<AuthResponse>.Ok(new AuthResponse(
                _tokens.GenerateAccessToken(user),
                newRefreshToken,
                user.RefreshTokenExpiresAt!.Value,
                user.Id,
                user.Email,
                user.Name
            ));
        }

        public async Task<Result<bool>> LogoutAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _user.GetByIdAsync(userId, ct);

            if (user is null)
                return Result<bool>.Fail(AppError.NotFound("Usuário não encontrado."));

            user.RevokeRefreshToken();
            await _user.UpdateAsync(user, ct);

            return Result<bool>.Ok(true);
        }
    }
}
