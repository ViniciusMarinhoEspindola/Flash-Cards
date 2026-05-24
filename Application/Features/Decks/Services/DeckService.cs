using Application.Common;
using Application.Features.Decks.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Decks.Services
{
    public class DeckService(
        IDeck _decks,
        IWorkspace _workspaces,
        IValidator<CreateDeckRequest> _createValidator)
    {
        public async Task<Result<IEnumerable<DeckResponse>>> GetAllByWorkspaceAsync(Guid workspaceId, Guid userId, CancellationToken ct = default)
        {
            var workspace = await _workspaces.GetByIdAsync(workspaceId, ct);

            if (workspace is null || workspace.UserId != userId)
                return Result<IEnumerable<DeckResponse>>.Fail(AppError.NotFound("Workspace não encontrado."));

            var decks = await _decks.GetAllByWorkspaceAsync(workspaceId, ct);
            return Result<IEnumerable<DeckResponse>>.Ok(decks.Select(ToResponse));
        }

        public async Task<Result<DeckResponse>> GetByIdAsync(Guid deckId, Guid userId, CancellationToken ct = default)
        {
            var deck = await _decks.GetByIdAsync(deckId, ct);

            if (deck is null || deck.Workspace.UserId != userId)
                return Result<DeckResponse>.Fail(AppError.NotFound("Deck não encontrado."));

            return Result<DeckResponse>.Ok(ToResponse(deck));
        }

        public async Task<Result<DeckResponse>> CreateAsync(Guid workspaceId, Guid userId, CreateDeckRequest request, CancellationToken ct = default)
        {
            var validation = await _createValidator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return validation.ToFailResult<DeckResponse>();

            var workspace = await _workspaces.GetByIdAsync(workspaceId, ct);

            if (workspace is null || workspace.UserId != userId)
                return Result<DeckResponse>.Fail(AppError.NotFound("Workspace não encontrado."));

            if (await _decks.ExistsByNameAsync(workspaceId, request.Name, ct))
                return Result<DeckResponse>.Fail(AppError.Conflict("Já existe um deck com esse nome neste workspace."));

            var deck = Deck.Create(workspaceId, request.Name);
            await _decks.AddAsync(deck, ct);

            return Result<DeckResponse>.Ok(ToResponse(deck));
        }

        public async Task<Result<bool>> DeleteAsync(Guid deckId, Guid userId, CancellationToken ct = default)
        {
            var deck = await _decks.GetByIdAsync(deckId, ct);

            if (deck is null || deck.Workspace.UserId != userId)
                return Result<bool>.Fail(AppError.NotFound("Deck não encontrado."));

            await _decks.DeleteAsync(deck, ct);
            return Result<bool>.Ok(true);
        }

        private static DeckResponse ToResponse(Deck d) => new(d.Id, d.WorkspaceId, d.Name, d.CreatedAt);
    }
}
