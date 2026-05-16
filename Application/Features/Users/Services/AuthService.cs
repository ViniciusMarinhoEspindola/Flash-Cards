using Application.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Features.Users.Services
{
    public class AuthService(IUser _user)
    {
        public async Task<string> CheckAuthenticationAsync(Guid userId, CancellationToken ct = default)
        {
            var user = await _user.GetByIdAsync(userId, ct);

            if (user is null)
                return "Authentication failed";

            return await Task.Run(() => "Authentication successful");
        }
    }
}
