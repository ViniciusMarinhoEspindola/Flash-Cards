using Application.Common;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Features.Users.Services
{
    public class AuthService(IUserRepository _userRepository)
    {
        public async Task<Result<IEnumerable<User>>> GetByUserAsync(Guid userId, CancellationToken ct = default)
        {
            var response = await _userRepository.GetByUserId(userId);

            return Result<IEnumerable<User>>.Ok(response);
        }
    }
}
