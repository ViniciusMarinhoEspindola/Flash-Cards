using Domain.Entities;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.IntegrationTests.Repositories;

public class StudySessionRepositoryTests : IDisposable
{
    private readonly DBContext _db;
    private readonly StudySessionRepository _sut;

    public StudySessionRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _db = new DBContext(options);
        _sut = new StudySessionRepository(_db);
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task AddAsync_ShouldPersist_StudySession()
    {
        var session = StudySession.Create(Guid.NewGuid());

        await _sut.AddAsync(session);

        _db.StudySessions.Should().ContainSingle(s => s.Id == session.Id);
    }

    [Fact]
    public async Task GetActiveByUserAsync_WhenNoSessionExists_ShouldReturnNull()
    {
        var found = await _sut.GetActiveByUserAsync(Guid.NewGuid());

        found.Should().BeNull();
    }

    [Fact]
    public async Task GetActiveByUserAsync_WhenActiveSessionExists_ShouldReturnIt()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        await _sut.AddAsync(session);

        var found = await _sut.GetActiveByUserAsync(userId);

        found.Should().NotBeNull();
        found!.Id.Should().Be(session.Id);
    }

    [Fact]
    public async Task GetActiveByUserAsync_WhenSessionAlreadyEnded_ShouldReturnNull()
    {
        var userId = Guid.NewGuid();
        var session = StudySession.Create(userId);
        session.End();
        await _sut.AddAsync(session);

        var found = await _sut.GetActiveByUserAsync(userId);

        found.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WhenSessionExists_ShouldReturnSession()
    {
        var session = StudySession.Create(Guid.NewGuid());
        await _sut.AddAsync(session);

        var found = await _sut.GetByIdAsync(session.Id);

        found.Should().NotBeNull();
        found!.Id.Should().Be(session.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WhenSessionNotExists_ShouldReturnNull()
    {
        var found = await _sut.GetByIdAsync(Guid.NewGuid());

        found.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldPersist_EndedAtAndCounters()
    {
        var session = StudySession.Create(Guid.NewGuid());
        await _sut.AddAsync(session);

        session.RecordAnswer(true);
        session.End();
        await _sut.UpdateAsync(session);

        var updated = await _sut.GetByIdAsync(session.Id);
        updated!.EndedAt.Should().NotBeNull();
        updated.CardsReviewed.Should().Be(1);
        updated.CorrectCount.Should().Be(1);
    }
}
