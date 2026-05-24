namespace Application.Contracts.AI.DTOs
{
    public record CorrectSentenceRequest(string Sentence, string Term, string? LanguageCode);
}
