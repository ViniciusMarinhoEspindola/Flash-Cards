using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UserRepository(DBContext _db) : IUser
    {
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
            => await _db.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant().Trim(), ct);

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
            => await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, ct);

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default)
            => await _db.Users.AnyAsync(u => u.Email == email.ToLowerInvariant().Trim(), ct);

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(user, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(User user, CancellationToken ct = default)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync(ct);
        }
    }
}
