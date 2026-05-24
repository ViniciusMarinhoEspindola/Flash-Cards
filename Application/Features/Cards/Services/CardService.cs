using Application.Common;
using Application.Contracts.AI;
using Application.Contracts.AI.DTOs;
using Application.Features.Cards.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Features.Cards.Services
{
    public class CardService(
        ICard _cards,
        IDeck _decks,
        IWorkspace _workspaces,
        ICardProgress _progress,
        IAiService _ai,
        IValidator<CreateCardRequest> _createValidator)
    {
        public async Task<Result<IEnumerable<CardResponse>>> GetAllByDeckAsync(Guid deckId, Guid userId, CancellationToken ct = default)
        {
            var deck = await _decks.GetByIdAsync(deckId, ct);

            if (deck is null || deck.Workspace.UserId != userId)
                return Result<IEnumerable<CardResponse>>.Fail(AppError.NotFound("Deck não encontrado."));

            var cards = await _cards.GetAllByDeckAsync(deckId, ct);
            return Result<IEnumerable<CardResponse>>.Ok(cards.Select(ToResponse));
        }

        public async Task<Result<CardResponse>> GetByIdAsync(Guid cardId, Guid userId, CancellationToken ct = default)
        {
            var card = await _cards.GetByIdAsync(cardId, ct);

            if (card is null)
                return Result<CardResponse>.Fail(AppError.NotFound("Card não encontrado."));

            var deck = await _decks.GetByIdAsync(card.DeckId, ct);
            if (deck is null || deck.Workspace.UserId != userId)
                return Result<CardResponse>.Fail(AppError.NotFound("Card não encontrado."));

            return Result<CardResponse>.Ok(ToResponse(card));
        }

        public async Task<Result<CardResponse>> CreateAsync(Guid userId, CreateCardRequest request, CancellationToken ct = default)
        {
            var validation = await _createValidator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return validation.ToFailResult<CardResponse>();

            var deck = await _decks.GetByIdAsync(request.DeckId, ct);
            if (deck is null || deck.Workspace.UserId != userId)
                return Result<CardResponse>.Fail(AppError.NotFound("Deck não encontrado."));

            var card = Card.Create(request.DeckId, request.Term, request.Definition, request.Romanization, request.IsPhrase, request.Source);
            await _cards.AddAsync(card, ct);

            if (request.Examples.Any())
            {
                var examples = request.Examples.Select(e => CardExample.Create(card.Id, e.Sentence, e.Note));
                foreach (var example in examples)
                    card.CardExamples.Add(example);
                await _cards.UpdateAsync(card, ct);
            }

            var progress = CardProgress.Create(card.Id, userId);
            await _progress.AddAsync(progress, ct);

            return Result<CardResponse>.Ok(ToResponse(card));
        }

        public async Task<Result<CardResponse>> CaptureAsync(Guid userId, CaptureTermRequest request, CancellationToken ct = default)
        {
            var deck = await _decks.GetByIdAsync(request.DeckId, ct);
            if (deck is null || deck.Workspace.UserId != userId)
                return Result<CardResponse>.Fail(AppError.NotFound("Deck não encontrado."));

            var workspace = await _workspaces.GetByIdAsync(deck.WorkspaceId, ct);

            var aiRequest = new GenerateCardRequest(
                request.Term,
                workspace?.Language?.Code,
                workspace?.NativeLanguage?.Code
            );

            var aiResponse = await _ai.GenerateCardAsync(aiRequest, ct);

            var card = Card.Create(request.DeckId, request.Term, aiResponse.Definition, aiResponse.Romanization, aiResponse.IsPhrase, CardSource.Agent);
            await _cards.AddAsync(card, ct);

            if (aiResponse.Examples.Any())
            {
                var examples = aiResponse.Examples.Select(e => CardExample.Create(card.Id, e.Sentence, e.Note));
                foreach (var example in examples)
                    card.CardExamples.Add(example);
                await _cards.UpdateAsync(card, ct);
            }

            var progress = CardProgress.Create(card.Id, userId);
            await _progress.AddAsync(progress, ct);

            return Result<CardResponse>.Ok(ToResponse(card));
        }

        public async Task<Result<bool>> DeleteAsync(Guid cardId, Guid userId, CancellationToken ct = default)
        {
            var card = await _cards.GetByIdAsync(cardId, ct);
            if (card is null)
                return Result<bool>.Fail(AppError.NotFound("Card não encontrado."));

            var deck = await _decks.GetByIdAsync(card.DeckId, ct);
            if (deck is null || deck.Workspace.UserId != userId)
                return Result<bool>.Fail(AppError.NotFound("Card não encontrado."));

            await _cards.DeleteAsync(card, ct);
            return Result<bool>.Ok(true);
        }

        private static CardResponse ToResponse(Card c) => new(
            c.Id,
            c.DeckId,
            c.Term,
            c.Definition,
            c.Romanization,
            c.IsPhrase,
            c.Source,
            c.CardExamples.Select(e => new CardExampleResponse(e.Id, e.Sentence, e.Note)),
            c.CreatedAt
        );
    }
}
