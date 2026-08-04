using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class StudySessionAnswer : BaseEntity
    {
        public Guid StudySessionId { get; private set; }
        public Guid CardId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public bool IsCorrect { get; private set; }
        public int LevelBefore { get; private set; }
        public int LevelAfter { get; private set; }
        public DateTime AnsweredAt { get; private set; }

        public StudySession StudySession { get; private set; } = null!;
        public Card Card { get; private set; } = null!;
        public User User { get; private set; } = null!;

        protected StudySessionAnswer() { }

        public static StudySessionAnswer Create(Guid studySessionId, Guid cardId, Guid userId, int rating, bool isCorrect, int levelBefore, int levelAfter)
        {
            return new StudySessionAnswer
            {
                StudySessionId = studySessionId,
                CardId = cardId,
                UserId = userId,
                Rating = rating,
                IsCorrect = isCorrect,
                LevelBefore = levelBefore,
                LevelAfter = levelAfter,
                AnsweredAt = DateTime.UtcNow
            };
        }
    }
}
