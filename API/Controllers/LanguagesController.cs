using API.Extensions;
using Application.Features.Languages.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguagesController(LanguageService _languageService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _languageService.GetAllAsync(ct);
            return result.ToActionResult(this);
        }
    }
}
