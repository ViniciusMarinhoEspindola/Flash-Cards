using API.Extensions;
using Application.Features.Users.DTOs;
using Application.Features.Users.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService _userService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _userService.GetByIdAsync(CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RegisterRequestDto registerRequestDto, CancellationToken ct)
        {
            var result = await _userService.CreateAsync(registerRequestDto, ct);
            return result.ToActionResult(this);
        }
    }
}
