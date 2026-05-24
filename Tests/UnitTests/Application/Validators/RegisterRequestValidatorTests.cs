using Application.Features.Users.DTOs;
using Application.Features.Users.Validators;
using Domain.Interfaces;

namespace FlashCards.UnitTests.Application.Validators;

public class RegisterRequestValidatorTests
{
    private readonly IUser _userRepository = Substitute.For<IUser>();
    private readonly RegisterRequestValidator _sut;

    public RegisterRequestValidatorTests()
    {
        _userRepository.ExistsByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(false);
        _sut = new RegisterRequestValidator(_userRepository);
    }

    [Fact]
    public async Task Validate_WithValidData_ShouldBeValid()
    {
        var dto = new RegisterRequestDto { Email = "test@example.com", Password = "password123" };

        var result = await _sut.ValidateAsync(dto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("missing@")]
    public async Task Validate_WithInvalidEmail_ShouldBeInvalid(string email)
    {
        var dto = new RegisterRequestDto { Email = email, Password = "password123" };

        var result = await _sut.ValidateAsync(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task Validate_WhenEmailAlreadyExists_ShouldBeInvalid()
    {
        _userRepository.ExistsByEmailAsync("existing@example.com", Arg.Any<CancellationToken>()).Returns(true);
        var dto = new RegisterRequestDto { Email = "existing@example.com", Password = "password123" };

        var result = await _sut.ValidateAsync(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "E-mail já cadastrado.");
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345")]
    public async Task Validate_WithInvalidPassword_ShouldBeInvalid(string password)
    {
        var dto = new RegisterRequestDto { Email = "test@example.com", Password = password };

        var result = await _sut.ValidateAsync(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password");
    }
}
