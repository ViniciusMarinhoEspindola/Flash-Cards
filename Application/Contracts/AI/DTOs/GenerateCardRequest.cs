namespace Application.Contracts.AI.DTOs
{
    public record GenerateCardRequest(string Term, string? LanguageCode, string? NativeLanguageCode);
}
