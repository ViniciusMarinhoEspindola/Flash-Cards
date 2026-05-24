using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class WorkspaceRepository(DBContext _db) : IWorkspace
    {
        public async Task<Workspace?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Workspaces
                .Include(w => w.Language)
                .Include(w => w.NativeLanguage)
                .FirstOrDefaultAsync(w => w.Id == id, ct);

        public async Task<IEnumerable<Workspace>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
            => await _db.Workspaces
                .Include(w => w.Language)
                .Include(w => w.NativeLanguage)
                .Where(w => w.UserId == userId)
                .OrderBy(w => w.Name)
                .ToListAsync(ct);

        public async Task<bool> ExistsByNameAsync(Guid userId, string name, CancellationToken ct = default)
            => await _db.Workspaces.AnyAsync(w => w.UserId == userId && w.Name == name.Trim(), ct);

        public async Task AddAsync(Workspace workspace, CancellationToken ct = default)
        {
            await _db.Workspaces.AddAsync(workspace, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Workspace workspace, CancellationToken ct = default)
        {
            _db.Workspaces.Update(workspace);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Workspace workspace, CancellationToken ct = default)
        {
            _db.Workspaces.Remove(workspace);
            await _db.SaveChangesAsync(ct);
        }
    }
}
