using Domain.Entities;

namespace FlashCards.UnitTests.Domain.Entities;

public class UserTests
{
    [Fact]
    public void Create_ShouldLowercase_Email()
    {
        var user = User.Create("TEST@EXAMPLE.COM", "Test User", "hash");

        user.Email.Should().Be("test@example.com");
    }

    [Fact]
    public void Create_ShouldTrim_Email()
    {
        var user = User.Create("  test@example.com  ", "Test User", "hash");

        user.Email.Should().Be("test@example.com");
    }

    [Fact]
    public void Create_ShouldTrim_Name()
    {
        var user = User.Create("test@example.com", "  Test User  ", "hash");

        user.Name.Should().Be("Test User");
    }

    [Fact]
    public void Create_ShouldSet_PasswordHash()
    {
        var user = User.Create("test@example.com", "Test User", "my-hash");

        user.PasswordHash.Should().Be("my-hash");
    }

    [Fact]
    public void Create_ShouldHave_EmptyWorkspaces()
    {
        var user = User.Create("test@example.com", "Test User", "hash");

        user.Workspaces.Should().BeEmpty();
    }

    [Fact]
    public void SetRefreshToken_ShouldSet_Token()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        var expiry = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken("my-token", expiry);

        user.RefreshToken.Should().Be("my-token");
    }

    [Fact]
    public void SetRefreshToken_ShouldSet_ExpiresAt()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        var expiry = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken("my-token", expiry);

        user.RefreshTokenExpiresAt.Should().Be(expiry);
    }

    [Fact]
    public void RevokeRefreshToken_ShouldNull_Token()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        user.SetRefreshToken("my-token", DateTime.UtcNow.AddDays(7));

        user.RevokeRefreshToken();

        user.RefreshToken.Should().BeNull();
    }

    [Fact]
    public void RevokeRefreshToken_ShouldNull_ExpiresAt()
    {
        var user = User.Create("test@example.com", "Test User", "hash");
        user.SetRefreshToken("my-token", DateTime.UtcNow.AddDays(7));

        user.RevokeRefreshToken();

        user.RefreshTokenExpiresAt.Should().BeNull();
    }
}
