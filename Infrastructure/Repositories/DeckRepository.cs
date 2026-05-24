using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class DeckRepository(DBContext _db) : IDeck
    {
        public async Task<Deck?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Decks
                .Include(d => d.Workspace)
                .FirstOrDefaultAsync(d => d.Id == id, ct);

        public async Task<IEnumerable<Deck>> GetAllByWorkspaceAsync(Guid workspaceId, CancellationToken ct = default)
            => await _db.Decks
                .Where(d => d.WorkspaceId == workspaceId)
                .OrderBy(d => d.Name)
                .ToListAsync(ct);

        public async Task<bool> ExistsByNameAsync(Guid workspaceId, string name, CancellationToken ct = default)
            => await _db.Decks.AnyAsync(d => d.WorkspaceId == workspaceId && d.Name == name.Trim(), ct);

        public async Task AddAsync(Deck deck, CancellationToken ct = default)
        {
            await _db.Decks.AddAsync(deck, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Deck deck, CancellationToken ct = default)
        {
            _db.Decks.Remove(deck);
            await _db.SaveChangesAsync(ct);
        }
    }
}
