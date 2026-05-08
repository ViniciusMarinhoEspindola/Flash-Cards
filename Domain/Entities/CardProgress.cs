using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CardProgress : BaseEntity
    {
        public Guid CardId { get; private set; }
        public Guid UserId { get; private set; }
        public int Level { get; private set; } = 0;
        public double Easiness { get; private set; } = 0.0;
        public int Interval { get; private set; } = 3;
        public int Repetitions { get; private set; } = 0;
        public DateTime NextReview { get; private set; } = DateTime.UtcNow.AddDays(3);
        public DateTime? LastReviewed { get; private set; }

        public Card Card { get; private set; } = null!;
        public User User { get; private set; } = null!;

        protected CardProgress() { }

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
    }
}
