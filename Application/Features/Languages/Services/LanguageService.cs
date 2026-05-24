using Application.Common;
using Application.Features.Languages.DTOs;
using Domain.Interfaces;

namespace Application.Features.Languages.Services
{
    public class LanguageService(ILanguage _languages)
    {
        public async Task<Result<IEnumerable<LanguageResponse>>> GetAllAsync(CancellationToken ct = default)
        {
            var languages = await _languages.GetAllAsync(ct);
            var response = languages.Select(l => new LanguageResponse(l.Id, l.Name, l.Code, l.FlagEmoji));
            return Result<IEnumerable<LanguageResponse>>.Ok(response);
        }
    }
}
