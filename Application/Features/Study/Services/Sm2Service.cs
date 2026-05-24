namespace Application.Features.Study.Services
{
    public static class Sm2Service
    {
        public static (int Level, double Easiness, int Interval, int Repetitions, DateTime NextReview)
            Calculate(int currentLevel, double easiness, int interval, int repetitions, int rating)
        {
            rating = Math.Clamp(rating, 0, 5);

            int newLevel;
            double newEasiness;
            int newInterval;
            int newRepetitions;

            if (rating >= 3)
            {
                newRepetitions = repetitions + 1;
                newInterval = repetitions switch
                {
                    0 => 1,
                    1 => 6,
                    _ => (int)Math.Round(interval * easiness)
                };
                newEasiness = Math.Max(1.3, easiness + 0.1 - (5 - rating) * (0.08 + (5 - rating) * 0.02));
                newLevel = Math.Min(6, currentLevel + 1);
            }
            else
            {
                newRepetitions = 0;
                newInterval = 1;
                newEasiness = easiness;
                // Never falls back to level 0 after being reviewed at least once
                newLevel = currentLevel > 0 ? Math.Max(1, currentLevel - 1) : 0;
            }

            var nextReview = DateTime.UtcNow.AddDays(newInterval);
            return (newLevel, newEasiness, newInterval, newRepetitions, nextReview);
        }
    }
}
