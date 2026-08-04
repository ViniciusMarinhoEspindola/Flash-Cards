using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class StudySession : BaseEntity
    {
        public Guid UserId { get; private set; }
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public int CardsReviewed { get; private set; } = 0;
        public int CorrectCount { get; private set; } = 0;
        public int IncorrectCount { get; private set; } = 0;

        public User User { get; private set; } = null!;
        public ICollection<StudySessionAnswer> Answers { get; private set; } = [];

        protected StudySession() { }

        public static StudySession Create(Guid userId)
        {
            return new StudySession
            {
                UserId = userId,
                StartedAt = DateTime.UtcNow
            };
        }

        public void RecordAnswer(bool isCorrect)
        {
            CardsReviewed++;
            if (isCorrect)
                CorrectCount++;
            else
                IncorrectCount++;
        }

        public void End()
        {
            EndedAt = DateTime.UtcNow;
        }
    }
}
