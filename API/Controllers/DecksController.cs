using API.Extensions;
using Application.Features.Decks.DTOs;
using Application.Features.Decks.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/workspaces/{workspaceId:guid}/decks")]
    [ApiController]
    [Authorize]
    public class DecksController(DeckService _deckService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid workspaceId, CancellationToken ct)
        {
            var result = await _deckService.GetAllByWorkspaceAsync(workspaceId, CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpGet("{deckId:guid}")]
        public async Task<IActionResult> GetById(Guid workspaceId, Guid deckId, CancellationToken ct)
        {
            var result = await _deckService.GetByIdAsync(deckId, CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid workspaceId, [FromBody] CreateDeckRequest request, CancellationToken ct)
        {
            var result = await _deckService.CreateAsync(workspaceId, CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }

        [HttpDelete("{deckId:guid}")]
        public async Task<IActionResult> Delete(Guid workspaceId, Guid deckId, CancellationToken ct)
        {
            var result = await _deckService.DeleteAsync(deckId, CurrentUserId, ct);
            return result.ToActionResult(this);
        }
    }
}
