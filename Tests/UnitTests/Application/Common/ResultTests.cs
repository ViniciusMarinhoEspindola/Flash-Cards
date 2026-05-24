using Application.Common;

namespace FlashCards.UnitTests.Application.Common;

public class ResultTests
{
    [Fact]
    public void Ok_ShouldSet_IsSuccessTrue()
    {
        var result = Result<string>.Ok("data");

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Ok_ShouldSet_Data()
    {
        var result = Result<string>.Ok("data");

        result.Data.Should().Be("data");
    }

    [Fact]
    public void Ok_Error_ShouldBeNull()
    {
        var result = Result<string>.Ok("data");

        result.Error.Should().BeNull();
    }

    [Fact]
    public void Fail_ShouldSet_IsSuccessFalse()
    {
        var result = Result<string>.Fail(AppError.NotFound("not found"));

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Fail_ShouldSet_Error()
    {
        var error = AppError.NotFound("not found");
        var result = Result<string>.Fail(error);

        result.Error.Should().Be(error);
    }

    [Fact]
    public void Fail_Data_ShouldBeNull()
    {
        var result = Result<string>.Fail(AppError.NotFound("not found"));

        result.Data.Should().BeNull();
    }

    [Theory]
    [InlineData(ErrorCode.NotFound)]
    [InlineData(ErrorCode.Unauthorized)]
    [InlineData(ErrorCode.Validation)]
    [InlineData(ErrorCode.Conflict)]
    public void AppError_Factories_ShouldSet_CorrectCode(ErrorCode expectedCode)
    {
        AppError error = expectedCode switch
        {
            ErrorCode.NotFound => AppError.NotFound("msg"),
            ErrorCode.Unauthorized => AppError.Unauthorized("msg"),
            ErrorCode.Validation => AppError.Validation("msg"),
            ErrorCode.Conflict => AppError.Conflict("msg"),
            _ => throw new InvalidOperationException()
        };

        error.Code.Should().Be(expectedCode);
        error.Message.Should().Be("msg");
    }
}
