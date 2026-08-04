using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStudySession
    {
        Task<StudySession?> GetActiveByUserAsync(Guid userId, CancellationToken ct = default);
        Task<StudySession?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(StudySession session, CancellationToken ct = default);
        Task UpdateAsync(StudySession session, CancellationToken ct = default);
    }
}
