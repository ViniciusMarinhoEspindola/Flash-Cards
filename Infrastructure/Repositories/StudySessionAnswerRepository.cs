using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;

namespace Infraestructure.Repositories
{
    public class StudySessionAnswerRepository(DBContext _db) : IStudySessionAnswer
    {
        public async Task AddAsync(StudySessionAnswer answer, CancellationToken ct = default)
        {
            await _db.StudySessionAnswers.AddAsync(answer, ct);
            await _db.SaveChangesAsync(ct);
        }
    }
}
