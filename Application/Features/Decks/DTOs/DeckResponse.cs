namespace Application.Features.Decks.DTOs
{
    public record DeckResponse(
        Guid Id,
        Guid WorkspaceId,
        string Name,
        DateTime CreatedAt);
}
