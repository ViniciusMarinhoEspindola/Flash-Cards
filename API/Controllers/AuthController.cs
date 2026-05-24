using API.Extensions;
using Application.Features.Users.DTOs;
using Application.Features.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(AuthService _authService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken ct)
        {
            var result = await _authService.RegisterAsync(request, ct);
            return result.ToActionResult(this);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var result = await _authService.LoginAsync(request, ct);
            return result.ToActionResult(this);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
        {
            var result = await _authService.RefreshTokenAsync(request, ct);
            return result.ToActionResult(this);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            var result = await _authService.LogoutAsync(CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Id = CurrentUserId,
                Email = User.FindFirstValue(ClaimTypes.Email),
                Name = User.FindFirstValue(ClaimTypes.Name)
            });
        }
    }
}
