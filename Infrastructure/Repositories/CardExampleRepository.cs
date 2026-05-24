using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CardExampleRepository(DBContext _db) : ICardExample
    {
        public async Task<IEnumerable<CardExample>> GetAllByCardAsync(Guid cardId, CancellationToken ct = default)
            => await _db.CardExamples
                .Where(e => e.CardId == cardId)
                .ToListAsync(ct);

        public async Task AddRangeAsync(IEnumerable<CardExample> examples, CancellationToken ct = default)
        {
            await _db.CardExamples.AddRangeAsync(examples, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteByCardAsync(Guid cardId, CancellationToken ct = default)
        {
            var examples = await _db.CardExamples.Where(e => e.CardId == cardId).ToListAsync(ct);
            _db.CardExamples.RemoveRange(examples);
            await _db.SaveChangesAsync(ct);
        }
    }
}
