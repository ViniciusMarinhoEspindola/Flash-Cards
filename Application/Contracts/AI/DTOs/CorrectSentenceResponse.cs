namespace Application.Contracts.AI.DTOs
{
    public record CorrectSentenceResponse(bool IsCorrect, string Feedback, string CorrectedSentence);
}
