using Application.Common;
using Application.Features.Study.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Features.Study.Services
{
    public class StudyService(ICardProgress _progress, IStudySession _sessions, IStudySessionAnswer _answers)
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

        public async Task<Result<StartSessionResponse>> StartSessionAsync(Guid userId, CancellationToken ct = default)
        {
            var active = await _sessions.GetActiveByUserAsync(userId, ct);
            if (active is not null)
                return Result<StartSessionResponse>.Ok(new StartSessionResponse(active.Id, active.StartedAt));

            var session = StudySession.Create(userId);
            await _sessions.AddAsync(session, ct);

            return Result<StartSessionResponse>.Ok(new StartSessionResponse(session.Id, session.StartedAt));
        }

        public async Task<Result<StudyCardResponse?>> SubmitAnswerAsync(Guid userId, ReviewAnswerRequest request, CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(request.SessionId, ct);
            if (session is null || session.UserId != userId || session.EndedAt is not null)
                return Result<StudyCardResponse?>.Fail(AppError.NotFound("Sessão de estudo não encontrada ou já encerrada."));

            var cardProgress = await _progress.GetByCardIdAsync(request.CardId, ct);

            if (cardProgress is null || cardProgress.UserId != userId)
                return Result<StudyCardResponse?>.Fail(AppError.NotFound("Card não encontrado."));

            var levelBefore = cardProgress.Level;

            var (newLevel, newEasiness, newInterval, newRepetitions, nextReview) = Sm2Service.Calculate(
                cardProgress.Level,
                cardProgress.Easiness,
                cardProgress.Interval,
                cardProgress.Repetitions,
                request.Rating
            );

            cardProgress.Update(newLevel, newEasiness, newInterval, newRepetitions, nextReview);
            await _progress.UpdateAsync(cardProgress, ct);

            var isCorrect = Sm2Service.IsCorrect(request.Rating);

            var answer = StudySessionAnswer.Create(session.Id, request.CardId, userId, request.Rating, isCorrect, levelBefore, newLevel);
            await _answers.AddAsync(answer, ct);

            session.RecordAnswer(isCorrect);
            await _sessions.UpdateAsync(session, ct);

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

        public async Task<Result<SessionSummaryResponse>> EndSessionAsync(Guid userId, Guid sessionId, CancellationToken ct = default)
        {
            var session = await _sessions.GetByIdAsync(sessionId, ct);
            if (session is null || session.UserId != userId)
                return Result<SessionSummaryResponse>.Fail(AppError.NotFound("Sessão de estudo não encontrada."));

            if (session.EndedAt is null)
            {
                session.End();
                await _sessions.UpdateAsync(session, ct);
            }

            return Result<SessionSummaryResponse>.Ok(new SessionSummaryResponse(
                session.Id,
                session.StartedAt,
                session.EndedAt,
                session.CardsReviewed,
                session.CorrectCount,
                session.IncorrectCount
            ));
        }
    }
}
