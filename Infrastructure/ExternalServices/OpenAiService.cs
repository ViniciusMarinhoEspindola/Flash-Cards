using Application.Contracts.AI;
using Application.Contracts.AI.DTOs;

namespace Infraestructure.ExternalServices
{
    public class OpenAiService : IAiService
    {
        public Task<GenerateCardResponse> GenerateCardAsync(GenerateCardRequest request, CancellationToken ct = default)
        {
            // Stub — OpenAI integration will be implemented in a future phase
            var response = new GenerateCardResponse(
                Definition: $"[Definição de '{request.Term}' — IA não configurada]",
                Romanization: null,
                IsPhrase: request.Term.Contains(' '),
                Examples: []
            );
            return Task.FromResult(response);
        }

        public Task<CorrectSentenceResponse> CorrectSentenceAsync(CorrectSentenceRequest request, CancellationToken ct = default)
        {
            var response = new CorrectSentenceResponse(
                IsCorrect: true,
                Feedback: "Correção de frases não configurada ainda.",
                CorrectedSentence: request.Sentence
            );
            return Task.FromResult(response);
        }
    }
}
