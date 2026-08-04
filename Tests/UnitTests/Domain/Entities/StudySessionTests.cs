using Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class StudySessionTests
{
    [Fact]
    public void Create_ShouldSet_UserId()
    {
        var userId = Guid.NewGuid();

        var session = StudySession.Create(userId);

        session.UserId.Should().Be(userId);
    }

    [Fact]
    public void Create_ShouldSet_StartedAt_AndLeaveEndedAtNull()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.StartedAt.Should().NotBe(default);
        session.EndedAt.Should().BeNull();
    }

    [Fact]
    public void Create_ShouldHave_ZeroedCounters()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.CardsReviewed.Should().Be(0);
        session.CorrectCount.Should().Be(0);
        session.IncorrectCount.Should().Be(0);
    }

    [Fact]
    public void RecordAnswer_WhenCorrect_ShouldIncrement_CardsReviewedAndCorrectCount()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.RecordAnswer(isCorrect: true);

        session.CardsReviewed.Should().Be(1);
        session.CorrectCount.Should().Be(1);
        session.IncorrectCount.Should().Be(0);
    }

    [Fact]
    public void RecordAnswer_WhenIncorrect_ShouldIncrement_CardsReviewedAndIncorrectCount()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.RecordAnswer(isCorrect: false);

        session.CardsReviewed.Should().Be(1);
        session.CorrectCount.Should().Be(0);
        session.IncorrectCount.Should().Be(1);
    }

    [Fact]
    public void RecordAnswer_CalledMultipleTimes_ShouldAccumulate()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.RecordAnswer(true);
        session.RecordAnswer(true);
        session.RecordAnswer(false);

        session.CardsReviewed.Should().Be(3);
        session.CorrectCount.Should().Be(2);
        session.IncorrectCount.Should().Be(1);
    }

    [Fact]
    public void End_ShouldSet_EndedAt()
    {
        var session = StudySession.Create(Guid.NewGuid());

        session.End();

        session.EndedAt.Should().NotBeNull();
    }
}
