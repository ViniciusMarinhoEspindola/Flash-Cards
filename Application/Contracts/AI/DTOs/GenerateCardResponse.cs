namespace Application.Contracts.AI.DTOs
{
    public record GenerateCardResponse(
        string Definition,
        string? Romanization,
        bool IsPhrase,
        IEnumerable<GeneratedExample> Examples);

    public record GeneratedExample(string Sentence, string Note);
}
