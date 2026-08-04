using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IStudySessionAnswer
    {
        Task AddAsync(StudySessionAnswer answer, CancellationToken ct = default);
    }
}
