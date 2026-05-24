using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class LanguageRepository(DBContext _db) : ILanguage
    {
        public async Task<Language?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Languages.FirstOrDefaultAsync(l => l.Id == id, ct);

        public async Task<IEnumerable<Language>> GetAllAsync(CancellationToken ct = default)
            => await _db.Languages.OrderBy(l => l.Name).ToListAsync(ct);
    }
}
