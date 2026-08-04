using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class StudySessionRepository(DBContext _db) : IStudySession
    {
        public async Task<StudySession?> GetActiveByUserAsync(Guid userId, CancellationToken ct = default)
            => await _db.StudySessions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.EndedAt == null, ct);

        public async Task<StudySession?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.StudySessions
                .FirstOrDefaultAsync(s => s.Id == id, ct);

        public async Task AddAsync(StudySession session, CancellationToken ct = default)
        {
            await _db.StudySessions.AddAsync(session, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(StudySession session, CancellationToken ct = default)
        {
            _db.StudySessions.Update(session);
            await _db.SaveChangesAsync(ct);
        }
    }
}
