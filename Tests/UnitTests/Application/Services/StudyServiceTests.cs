using Application.Common;
using Application.Features.Study.DTOs;
using Application.Features.Study.Services;
using Domain.Entities;
using Domain.Interfaces;

namespace FlashCards.UnitTests.Application.Services;

public class StudyServiceTests
{
    private readonly ICardProgress _progress = Substitute.For<ICardProgress>();
    private readonly IStudySession _sessions = Substitute.For<IStudySession>();
    private readonly IStudySessionAnswer _answers = Substitute.For<IStudySessionAnswer>();
    private readonly StudyService _sut;

    public StudyServiceTests()
    {
        _sut = new StudyService(_progress, _sessions, _answers);
    }

    [Fact]
    public async Task StartSessionAsync_WhenNoActiveSession_ShouldCreateNewSession()
    {
        var userId = Guid.NewGuid();
        _sessions.GetActiveByUserAsync(userId, Arg.Any<CancellationToken>()).Returns((StudySession?)null);

        var result = await _sut.StartSessionAsync(userId);

        result.IsSuccess.Should().BeTrue();
        await _sessions.Received(1).AddAsync(Arg.Any<StudySession>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task StartSessionAsync_WhenActiveSessionExists_ShouldReuseIt()
    {
        var userId = Guid.NewGuid();
        var existing = StudySession.Create(userId);
        _sessions.GetActiveByUserAsync(userId, Arg.Any<CancellationToken>()).Returns(existing);

        var result = await _sut.StartSessionAsync(userId);

        result.Data!.SessionId.Should().Be(existing.Id);
        await _sessions.DidNotReceive().AddAsync(Arg.Any<StudySession>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenSessionNotFound_ShouldReturnNotFoundError()
    {
        var userId = Guid.NewGuid();
        var request = new ReviewAnswerRequest { SessionId = Guid.NewGuid(), CardId = Guid.NewGuid(), Rating = 4 };
        _sessions.GetByIdAsync(request.SessionId, Arg.Any<CancellationToken>()).Returns((StudySession?)null);

        var result = await _sut.SubmitAnswerAsync(userId, request);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenSessionBelongsToAnotherUser_ShouldReturnNotFoundError()
    {
        var session = StudySession.Create(Guid.NewGuid());
        var request = new ReviewAnswerRequest { SessionId = session.Id, CardId = Guid.NewGuid(), Rating = 4 };
        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);

        var result = await _sut.SubmitAnswerAsync(Guid.NewGuid(), request);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenSessionAlreadyEnded_ShouldReturnNotFoundError()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        session.End();
        var request = new ReviewAnswerRequest { SessionId = session.Id, CardId = Guid.NewGuid(), Rating = 4 };
        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);

        var result = await _sut.SubmitAnswerAsync(userId, request);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenCardProgressNotFound_ShouldReturnNotFoundError()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        var request = new ReviewAnswerRequest { SessionId = session.Id, CardId = Guid.NewGuid(), Rating = 4 };
        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);
        _progress.GetByCardIdAsync(request.CardId, Arg.Any<CancellationToken>()).Returns((CardProgress?)null);

        var result = await _sut.SubmitAnswerAsync(userId, request);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenValid_ShouldUpdateProgress_RecordAnswer_AndUpdateSession()
    {
        var userId = Guid.NewGuid();
        var cardId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        var cardProgress = CardProgress.Create(cardId, userId);
        var request = new ReviewAnswerRequest { SessionId = session.Id, CardId = cardId, Rating = 4 };

        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);
        _progress.GetByCardIdAsync(cardId, Arg.Any<CancellationToken>()).Returns(cardProgress);
        _progress.GetDueByUserAsync(userId, 1, Arg.Any<CancellationToken>()).Returns([]);

        var result = await _sut.SubmitAnswerAsync(userId, request);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeNull();
        await _progress.Received(1).UpdateAsync(cardProgress, Arg.Any<CancellationToken>());
        await _answers.Received(1).AddAsync(Arg.Any<StudySessionAnswer>(), Arg.Any<CancellationToken>());
        await _sessions.Received(1).UpdateAsync(session, Arg.Any<CancellationToken>());
        session.CardsReviewed.Should().Be(1);
        session.CorrectCount.Should().Be(1);
    }

    [Fact]
    public async Task SubmitAnswerAsync_WhenRatingIsBelowThree_ShouldRecordIncorrectAnswer()
    {
        var userId = Guid.NewGuid();
        var cardId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        var cardProgress = CardProgress.Create(cardId, userId);
        var request = new ReviewAnswerRequest { SessionId = session.Id, CardId = cardId, Rating = 1 };

        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);
        _progress.GetByCardIdAsync(cardId, Arg.Any<CancellationToken>()).Returns(cardProgress);
        _progress.GetDueByUserAsync(userId, 1, Arg.Any<CancellationToken>()).Returns([]);

        await _sut.SubmitAnswerAsync(userId, request);

        session.CardsReviewed.Should().Be(1);
        session.IncorrectCount.Should().Be(1);
        session.CorrectCount.Should().Be(0);
    }

    [Fact]
    public async Task EndSessionAsync_WhenSessionNotFound_ShouldReturnNotFoundError()
    {
        var userId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        _sessions.GetByIdAsync(sessionId, Arg.Any<CancellationToken>()).Returns((StudySession?)null);

        var result = await _sut.EndSessionAsync(userId, sessionId);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task EndSessionAsync_WhenActive_ShouldEndAndReturnSummary()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        session.RecordAnswer(true);
        session.RecordAnswer(false);
        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);

        var result = await _sut.EndSessionAsync(userId, session.Id);

        result.IsSuccess.Should().BeTrue();
        result.Data!.CardsReviewed.Should().Be(2);
        result.Data!.CorrectCount.Should().Be(1);
        result.Data!.IncorrectCount.Should().Be(1);
        result.Data!.EndedAt.Should().NotBeNull();
        await _sessions.Received(1).UpdateAsync(session, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task EndSessionAsync_WhenAlreadyEnded_ShouldNotUpdateAgain()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        session.End();
        _sessions.GetByIdAsync(session.Id, Arg.Any<CancellationToken>()).Returns(session);

        var result = await _sut.EndSessionAsync(userId, session.Id);

        result.IsSuccess.Should().BeTrue();
        await _sessions.DidNotReceive().UpdateAsync(Arg.Any<StudySession>(), Arg.Any<CancellationToken>());
    }
}
