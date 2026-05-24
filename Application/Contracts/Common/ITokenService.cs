using Domain.Entities;

namespace Application.Contracts.Common
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
