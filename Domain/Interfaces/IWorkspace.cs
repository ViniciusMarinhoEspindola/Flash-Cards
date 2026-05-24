using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IWorkspace
    {
        Task<Workspace?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Workspace>> GetAllByUserAsync(Guid userId, CancellationToken ct = default);
        Task<bool> ExistsByNameAsync(Guid userId, string name, CancellationToken ct = default);
        Task AddAsync(Workspace workspace, CancellationToken ct = default);
        Task UpdateAsync(Workspace workspace, CancellationToken ct = default);
        Task DeleteAsync(Workspace workspace, CancellationToken ct = default);
    }
}
