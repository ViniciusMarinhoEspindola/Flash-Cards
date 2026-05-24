using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILanguage
    {
        Task<Language?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Language>> GetAllAsync(CancellationToken ct = default);
    }
}
