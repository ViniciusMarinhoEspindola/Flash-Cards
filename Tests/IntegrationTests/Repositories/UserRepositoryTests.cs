using Domain.Entities;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.IntegrationTests.Repositories;

public class UserRepositoryTests : IDisposable
{
    private readonly DBContext _db;
    private readonly UserRepository _sut;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _db = new DBContext(options);
        _sut = new UserRepository(_db);
    }

    public void Dispose() => _db.Dispose();

    [Fact]
    public async Task AddAsync_ShouldPersist_User()
    {
        var user = User.Create("test@example.com", "Test User", "hash");

        await _sut.AddAsync(user);

        _db.Users.Should().ContainSingle(u => u.Email == "test@example.com");
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserExists_ShouldReturnUser()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        await _sut.AddAsync(user);

        var found = await _sut.GetByIdAsync(user.Id);

        found.Should().NotBeNull();
        found!.Email.Should().Be("test@example.com");
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserNotExists_ShouldReturnNull()
    {
        var found = await _sut.GetByIdAsync(Guid.NewGuid());

        found.Should().BeNull();
    }

    [Fact]
    public async Task GetByEmailAsync_WhenUserExists_ShouldReturnUser()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        await _sut.AddAsync(user);

        var found = await _sut.GetByEmailAsync("TEST@EXAMPLE.COM");

        found.Should().NotBeNull();
        found!.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task ExistsByEmailAsync_WhenUserExists_ShouldReturnTrue()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        await _sut.AddAsync(user);

        var exists = await _sut.ExistsByEmailAsync("test@example.com");

        exists.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsByEmailAsync_WhenUserNotExists_ShouldReturnFalse()
    {
        var exists = await _sut.ExistsByEmailAsync("notfound@example.com");

        exists.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_RefreshToken()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        await _sut.AddAsync(user);
        user.SetRefreshToken("new-token", DateTime.UtcNow.AddDays(7));

        await _sut.UpdateAsync(user);

        var updated = await _sut.GetByIdAsync(user.Id);
        updated!.RefreshToken.Should().Be("new-token");
    }
}
