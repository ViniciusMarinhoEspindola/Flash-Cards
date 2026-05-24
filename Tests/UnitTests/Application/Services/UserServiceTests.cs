using Application.Common;
using Application.Features.Users.DTOs;
using Application.Features.Users.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace FlashCards.UnitTests.Application.Services;

public class UserServiceTests
{
    private readonly IUser _userRepository = Substitute.For<IUser>();
    private readonly IValidator<RegisterRequestDto> _validator = Substitute.For<IValidator<RegisterRequestDto>>();
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _validator);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserExists_ShouldReturnOkResult()
    {
        var userId = Guid.NewGuid();
        var user = User.Create("test@example.com", "Test User", "hash");
        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _sut.GetByIdAsync(userId);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(user);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserNotFound_ShouldReturnNotFoundError()
    {
        var userId = Guid.NewGuid();
        _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User?)null);

        var result = await _sut.GetByIdAsync(userId);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.NotFound);
    }

    [Fact]
    public async Task CreateAsync_WhenValidationSucceeds_ShouldReturnOkResult()
    {
        var dto = new RegisterRequestDto { Email = "test@example.com", Password = "password123" };
        _validator
            .ValidateAsync(Arg.Any<RegisterRequestDto>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ValidationResult()));

        var result = await _sut.CreateAsync(dto);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        await _userRepository.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateAsync_WhenValidationFails_ShouldReturnValidationError()
    {
        var dto = new RegisterRequestDto { Email = "bad", Password = "" };
        var failures = new List<ValidationFailure> { new("Email", "E-mail inválido.") };
        _validator
            .ValidateAsync(Arg.Any<RegisterRequestDto>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new ValidationResult(failures)));

        var result = await _sut.CreateAsync(dto);

        result.IsSuccess.Should().BeFalse();
        result.Error!.Code.Should().Be(ErrorCode.Validation);
        await _userRepository.DidNotReceive().AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }
}
