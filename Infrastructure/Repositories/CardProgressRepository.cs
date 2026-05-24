using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CardProgressRepository(DBContext _db) : ICardProgress
    {
        public async Task<CardProgress?> GetByCardIdAsync(Guid cardId, CancellationToken ct = default)
            => await _db.CardProgresses
                .Include(cp => cp.Card)
                    .ThenInclude(c => c.CardExamples)
                .FirstOrDefaultAsync(cp => cp.CardId == cardId, ct);

        public async Task<IEnumerable<CardProgress>> GetDueByUserAsync(Guid userId, int limit, CancellationToken ct = default)
            => await _db.CardProgresses
                .Include(cp => cp.Card)
                    .ThenInclude(c => c.CardExamples)
                .Where(cp => cp.UserId == userId && cp.NextReview <= DateTime.UtcNow)
                .OrderBy(cp => cp.NextReview)
                .Take(limit)
                .ToListAsync(ct);

        public async Task AddAsync(CardProgress progress, CancellationToken ct = default)
        {
            await _db.CardProgresses.AddAsync(progress, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(CardProgress progress, CancellationToken ct = default)
        {
            _db.CardProgresses.Update(progress);
            await _db.SaveChangesAsync(ct);
        }
    }
}
