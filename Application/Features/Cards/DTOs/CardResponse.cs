using Domain.Enums;

namespace Application.Features.Cards.DTOs
{
    public record CardResponse(
        Guid Id,
        Guid DeckId,
        string Term,
        string Definition,
        string? Romanization,
        bool IsPhrase,
        CardSource Source,
        IEnumerable<CardExampleResponse> Examples,
        DateTime CreatedAt);

    public record CardExampleResponse(Guid Id, string Sentence, string Note);
}
