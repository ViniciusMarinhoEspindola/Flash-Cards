using Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class StudySessionAnswerTests
{
    [Fact]
    public void Create_ShouldSet_AllProperties()
    {
        var sessionId = Guid.NewGuid();
        var cardId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var answer = StudySessionAnswer.Create(sessionId, cardId, userId, rating: 4, isCorrect: true, levelBefore: 1, levelAfter: 2);

        answer.StudySessionId.Should().Be(sessionId);
        answer.CardId.Should().Be(cardId);
        answer.UserId.Should().Be(userId);
        answer.Rating.Should().Be(4);
        answer.IsCorrect.Should().BeTrue();
        answer.LevelBefore.Should().Be(1);
        answer.LevelAfter.Should().Be(2);
        answer.AnsweredAt.Should().NotBe(default);
    }

    [Fact]
    public void Create_WithIncorrectAnswer_ShouldSet_IsCorrectFalse()
    {
        var answer = StudySessionAnswer.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), rating: 1, isCorrect: false, levelBefore: 2, levelAfter: 1);

        answer.IsCorrect.Should().BeFalse();
    }
}
