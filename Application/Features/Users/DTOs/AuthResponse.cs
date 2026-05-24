namespace Application.Features.Users.DTOs
{
    public record AuthResponse(
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt,
        Guid UserId,
        string Email,
        string Name);
}
