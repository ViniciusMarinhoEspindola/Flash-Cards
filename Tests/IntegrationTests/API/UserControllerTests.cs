using System.Net;
using System.Net.Http.Json;
using Application.Features.Users.DTOs;
using FlashCards.IntegrationTests.Fixtures;

namespace FlashCards.IntegrationTests.API;

public class UserControllerTests : IClassFixture<ApplicationFactory>
{
    private readonly HttpClient _client;

    public UserControllerTests(ApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_WithValidData_ShouldReturn200()
    {
        var dto = new RegisterRequestDto { Email = "newuser@example.com", Password = "password123" };

        var response = await _client.PostAsJsonAsync("/api/user/Create", dto);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_WithInvalidEmail_ShouldReturn400()
    {
        var dto = new RegisterRequestDto { Email = "not-an-email", Password = "password123" };

        var response = await _client.PostAsJsonAsync("/api/user/Create", dto);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Create_WithShortPassword_ShouldReturn400()
    {
        var dto = new RegisterRequestDto { Email = "shortpass@example.com", Password = "123" };

        var response = await _client.PostAsJsonAsync("/api/user/Create", dto);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Create_WithDuplicateEmail_ShouldReturn400()
    {
        var dto = new RegisterRequestDto { Email = "duplicate@example.com", Password = "password123" };
        await _client.PostAsJsonAsync("/api/user/Create", dto);

        var response = await _client.PostAsJsonAsync("/api/user/Create", dto);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
