using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICard
    {
        Task<Card?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Card>> GetAllByDeckAsync(Guid deckId, CancellationToken ct = default);
        Task AddAsync(Card card, CancellationToken ct = default);
        Task UpdateAsync(Card card, CancellationToken ct = default);
        Task DeleteAsync(Card card, CancellationToken ct = default);
    }
}
