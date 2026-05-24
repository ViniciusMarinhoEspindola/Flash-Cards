using API.Extensions;
using Application.Features.Workspaces.DTOs;
using Application.Features.Workspaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/workspaces")]
    [ApiController]
    [Authorize]
    public class WorkspacesController(WorkspaceService _workspaceService) : ControllerBase
    {
        private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await _workspaceService.GetAllByUserAsync(CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await _workspaceService.GetByIdAsync(id, CurrentUserId, ct);
            return result.ToActionResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkspaceRequest request, CancellationToken ct)
        {
            var result = await _workspaceService.CreateAsync(CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkspaceRequest request, CancellationToken ct)
        {
            var result = await _workspaceService.UpdateAsync(id, CurrentUserId, request, ct);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await _workspaceService.DeleteAsync(id, CurrentUserId, ct);
            return result.ToActionResult(this);
        }
    }
}
