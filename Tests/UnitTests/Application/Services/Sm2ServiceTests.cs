using Application.Features.Study.Services;

namespace FlashCards.UnitTests.Application.Services;

public class Sm2ServiceTests
{
    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void IsCorrect_WhenRatingIsThreeOrAbove_ShouldReturnTrue(int rating)
    {
        Sm2Service.IsCorrect(rating).Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void IsCorrect_WhenRatingIsBelowThree_ShouldReturnFalse(int rating)
    {
        Sm2Service.IsCorrect(rating).Should().BeFalse();
    }
}
