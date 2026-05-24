using Application.Contracts.AI.DTOs;

namespace Application.Contracts.AI
{
    public interface IAiService
    {
        Task<GenerateCardResponse> GenerateCardAsync(GenerateCardRequest request, CancellationToken ct = default);
        Task<CorrectSentenceResponse> CorrectSentenceAsync(CorrectSentenceRequest request, CancellationToken ct = default);
    }
}
