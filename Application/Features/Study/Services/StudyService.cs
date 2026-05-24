using Application.Common;
using Application.Features.Study.DTOs;
using Domain.Interfaces;

namespace Application.Features.Study.Services
{
    public class StudyService(ICardProgress _progress)
    {
        private const int DefaultLimit = 20;

        public async Task<Result<IEnumerable<StudyCardResponse>>> GetDueCardsAsync(Guid userId, int limit = DefaultLimit, CancellationToken ct = default)
        {
            var dueProgresses = await _progress.GetDueByUserAsync(userId, limit, ct);

            var cards = dueProgresses.Select(cp => new StudyCardResponse(
                cp.CardId,
                cp.Card.Term,
                cp.Card.Definition,
                cp.Card.Romanization,
                cp.Card.IsPhrase,
                cp.Level,
                cp.Card.CardExamples.Select(e => new StudyExampleResponse(e.Sentence, e.Note))
            ));

            return Result<IEnumerable<StudyCardResponse>>.Ok(cards);
        }

        public async Task<Result<StudyCardResponse?>> SubmitAnswerAsync(Guid userId, ReviewAnswerRequest request, CancellationToken ct = default)
        {
            var cardProgress = await _progress.GetByCardIdAsync(request.CardId, ct);

            if (cardProgress is null || cardProgress.UserId != userId)
                return Result<StudyCardResponse?>.Fail(AppError.NotFound("Card não encontrado."));

            var (newLevel, newEasiness, newInterval, newRepetitions, nextReview) = Sm2Service.Calculate(
                cardProgress.Level,
                cardProgress.Easiness,
                cardProgress.Interval,
                cardProgress.Repetitions,
                request.Rating
            );

            cardProgress.Update(newLevel, newEasiness, newInterval, newRepetitions, nextReview);
            await _progress.UpdateAsync(cardProgress, ct);

            var nextDue = await _progress.GetDueByUserAsync(userId, 1, ct);
            var next = nextDue.FirstOrDefault();

            if (next is null)
                return Result<StudyCardResponse?>.Ok(null);

            return Result<StudyCardResponse?>.Ok(new StudyCardResponse(
                next.CardId,
                next.Card.Term,
                next.Card.Definition,
                next.Card.Romanization,
                next.Card.IsPhrase,
                next.Level,
                next.Card.CardExamples.Select(e => new StudyExampleResponse(e.Sentence, e.Note))
            ));
        }
    }
}
