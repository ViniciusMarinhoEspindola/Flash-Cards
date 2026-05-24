using API.Extensions;
using Application.Features.Cards.DTOs;
using Application.Features.Cards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [Authorize]
    public class CardsController(CardService _cardService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet("deck/{deckId:guid}")]
        public async Task<IActionResult> GetByDeck(Guid deckId, CancellationToken ct)
        {
            var result = await _cardService.GetAllByDeckAsync(deckId, CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _cardService.GetByIdAsync(id, CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardRequest request, CancellationToken ct)
        {
            var result = await _cardService.CreateAsync(CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }

        [HttpPost("capture")]
        public async Task<IActionResult> Capture([FromBody] CaptureTermRequest request, CancellationToken ct)
        {
            var result = await _cardService.CaptureAsync(CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _cardService.DeleteAsync(id, CurrentUserId, ct);
            return result.ToActionResult(this);
        }
    }
}
