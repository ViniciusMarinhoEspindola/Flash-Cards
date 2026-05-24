using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICardExample
    {
        Task<IEnumerable<CardExample>> GetAllByCardAsync(Guid cardId, CancellationToken ct = default);
        Task AddRangeAsync(IEnumerable<CardExample> examples, CancellationToken ct = default);
        Task DeleteByCardAsync(Guid cardId, CancellationToken ct = default);
    }
}
