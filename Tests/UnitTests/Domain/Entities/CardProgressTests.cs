using Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class CardProgressTests
{
    [Fact]
    public void Create_ShouldSet_AllProperties()
    {
        var cardId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var nextReview = DateTime.UtcNow.AddDays(5);
        var lastReviewed = DateTime.UtcNow;

        var progress = CardProgress.Create(cardId, userId, 2, 2.5, 10, 3, nextReview, lastReviewed);

        progress.CardId.Should().Be(cardId);
        progress.UserId.Should().Be(userId);
        progress.Level.Should().Be(2);
        progress.Easiness.Should().Be(2.5);
        progress.Interval.Should().Be(10);
        progress.Repetitions.Should().Be(3);
        progress.NextReview.Should().Be(nextReview);
        progress.LastReviewed.Should().Be(lastReviewed);
    }

    [Fact]
    public void Create_WithNullLastReviewed_ShouldSet_Null()
    {
        var progress = CardProgress.Create(Guid.NewGuid(), Guid.NewGuid(), 0, 0.0, 3, 0, DateTime.UtcNow, null);

        progress.LastReviewed.Should().BeNull();
    }
}
