using API.Extensions;
using Application.Features.Study.DTOs;
using Application.Features.Study.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/study")]
    [ApiController]
    [Authorize]
    public class StudyController(StudyService _studyService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet("due")]
        public async Task<IActionResult> GetDue([FromQuery] int limit = 20, CancellationToken ct = default)
        {
            var result = await _studyService.GetDueCardsAsync(CurrentUserId, limit, ct);
            return result.ToActionResult(this);
        }

        [HttpPost("answer")]
        public async Task<IActionResult> SubmitAnswer([FromBody] ReviewAnswerRequest request, CancellationToken ct)
        {
            var result = await _studyService.SubmitAnswerAsync(CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }
    }
}
