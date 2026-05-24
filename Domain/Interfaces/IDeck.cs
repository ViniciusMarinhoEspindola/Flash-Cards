using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeck
    {
        Task<Deck?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Deck>> GetAllByWorkspaceAsync(Guid workspaceId, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(Guid workspaceId, string name, CancellationToken ct = default);
        Task AddAsync(Deck deck, CancellationToken ct = default);
        Task DeleteAsync(Deck deck, CancellationToken ct = default);
    }
}
