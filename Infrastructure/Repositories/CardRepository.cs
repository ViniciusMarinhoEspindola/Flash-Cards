using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CardRepository(DBContext _db) : ICard
    {
        public async Task<Card?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Cards
                .Include(c => c.CardExamples)
                .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<IEnumerable<Card>> GetAllByDeckAsync(Guid deckId, CancellationToken ct = default)
            => await _db.Cards
                .Include(c => c.CardExamples)
                .Where(c => c.DeckId == deckId)
                .OrderBy(c => c.Term)
                .ToListAsync(ct);

        public async Task AddAsync(Card card, CancellationToken ct = default)
        {
            await _db.Cards.AddAsync(card, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Card card, CancellationToken ct = default)
        {
            _db.Cards.Update(card);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Card card, CancellationToken ct = default)
        {
            _db.Cards.Remove(card);
            await _db.SaveChangesAsync(ct);
        }
    }
}
