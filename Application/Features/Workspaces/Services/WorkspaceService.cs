using Application.Common;
using Application.Features.Workspaces.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Workspaces.Services
{
    public class WorkspaceService(
        IWorkspace _workspaces,
        IValidator<CreateWorkspaceRequest> _createValidator)
    {
        public async Task<Result<IEnumerable<WorkspaceResponse>>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
        {
            var workspaces = await _workspaces.GetAllByUserAsync(userId, ct);
            return Result<IEnumerable<WorkspaceResponse>>.Ok(workspaces.Select(ToResponse));
        }

        public async Task<Result<WorkspaceResponse>> GetByIdAsync(Guid workspaceId, Guid userId, CancellationToken ct = default)
        {
            var workspace = await _workspaces.GetByIdAsync(workspaceId, ct);

            if (workspace is null || workspace.UserId != userId)
                return Result<WorkspaceResponse>.Fail(AppError.NotFound("Workspace não encontrado."));

            return Result<WorkspaceResponse>.Ok(ToResponse(workspace));
        }

        public async Task<Result<WorkspaceResponse>> CreateAsync(Guid userId, CreateWorkspaceRequest request, CancellationToken ct = default)
        {
            var validation = await _createValidator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return validation.ToFailResult<WorkspaceResponse>();

            if (await _workspaces.ExistsByNameAsync(userId, request.Name, ct))
                return Result<WorkspaceResponse>.Fail(AppError.Conflict("Já existe um workspace com esse nome."));

            var workspace = Workspace.Create(userId, request.Name, request.LanguageId, request.NativeLanguageId);
            await _workspaces.AddAsync(workspace, ct);

            workspace = (await _workspaces.GetByIdAsync(workspace.Id, ct))!;
            return Result<WorkspaceResponse>.Ok(ToResponse(workspace));
        }

        public async Task<Result<WorkspaceResponse>> UpdateAsync(Guid workspaceId, Guid userId, UpdateWorkspaceRequest request, CancellationToken ct = default)
        {
            var workspace = await _workspaces.GetByIdAsync(workspaceId, ct);

            if (workspace is null || workspace.UserId != userId)
                return Result<WorkspaceResponse>.Fail(AppError.NotFound("Workspace não encontrado."));

            if (await _workspaces.ExistsByNameAsync(userId, request.Name, ct) && workspace.Name != request.Name.Trim())
                return Result<WorkspaceResponse>.Fail(AppError.Conflict("Já existe um workspace com esse nome."));

            workspace.Update(request.Name);
            await _workspaces.UpdateAsync(workspace, ct);

            return Result<WorkspaceResponse>.Ok(ToResponse(workspace));
        }

        public async Task<Result<bool>> DeleteAsync(Guid workspaceId, Guid userId, CancellationToken ct = default)
        {
            var workspace = await _workspaces.GetByIdAsync(workspaceId, ct);

            if (workspace is null || workspace.UserId != userId)
                return Result<bool>.Fail(AppError.NotFound("Workspace não encontrado."));

            await _workspaces.DeleteAsync(workspace, ct);
            return Result<bool>.Ok(true);
        }

        private static WorkspaceResponse ToResponse(Workspace w) => new(
            w.Id,
            w.Name,
            w.LanguageId,
            w.Language?.Name,
            w.Language?.Code,
            w.Language?.FlagEmoji,
            w.NativeLanguageId,
            w.NativeLanguage?.Name,
            w.CreatedAt
        );
    }
}
