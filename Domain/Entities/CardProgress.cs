using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class CardProgress : BaseEntity
    {
        public Guid CardId { get; private set; }
        public Guid UserId { get; private set; }
        public int Level { get; private set; } = 0;
        public double Easiness { get; private set; } = 2.5;
        public int Interval { get; private set; } = 1;
        public int Repetitions { get; private set; } = 0;
        public DateTime NextReview { get; private set; } = DateTime.UtcNow;
        public DateTime? LastReviewed { get; private set; }

        public Card Card { get; private set; } = null!;
        public User User { get; private set; } = null!;

        protected CardProgress() { }

        public static CardProgress Create(Guid cardId, Guid userId)
        {
            return new CardProgress
            {
                CardId = cardId,
                UserId = userId,
                Level = 0,
                Easiness = 2.5,
                Interval = 1,
                Repetitions = 0,
                NextReview = DateTime.UtcNow,
                LastReviewed = null
            };
        }

        public static CardProgress Create(Guid cardId, Guid userId, int level, double easiness, int interval, int repetitions, DateTime nextReview, DateTime? lastReviewed)
        {
            return new CardProgress
            {
                CardId = cardId,
                UserId = userId,
                Level = level,
                Easiness = easiness,
                Interval = interval,
                Repetitions = repetitions,
                NextReview = nextReview,
                LastReviewed = lastReviewed
            };
        }

        public void Update(int level, double easiness, int interval, int repetitions, DateTime nextReview)
        {
            Level = level;
            Easiness = easiness;
            Interval = interval;
            Repetitions = repetitions;
            NextReview = nextReview;
            LastReviewed = DateTime.UtcNow;
        }
    }
}
