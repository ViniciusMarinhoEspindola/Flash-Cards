namespace Application.Features.Study.DTOs
{
    public record StudyCardResponse(
        Guid CardId,
        string Term,
        string Definition,
        string? Romanization,
        bool IsPhrase,
        int Level,
        IEnumerable<StudyExampleResponse> Examples);

    public record StudyExampleResponse(string Sentence, string Note);
}
