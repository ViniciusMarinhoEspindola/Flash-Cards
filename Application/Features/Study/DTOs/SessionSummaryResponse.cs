namespace Application.Features.Study.DTOs
{
    public record SessionSummaryResponse(
        Guid SessionId,
        DateTime StartedAt,
        DateTime? EndedAt,
        int CardsReviewed,
        int CorrectCount,
        int IncorrectCount);
}
