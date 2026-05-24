using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICardProgress
    {
        Task<CardProgress?> GetByCardIdAsync(Guid cardId, CancellationToken ct = default);
        Task<IEnumerable<CardProgress>> GetDueByUserAsync(Guid userId, int limit, CancellationToken ct = default);
        Task AddAsync(CardProgress progress, CancellationToken ct = default);
        Task UpdateAsync(CardProgress progress, CancellationToken ct = default);
    }
}
