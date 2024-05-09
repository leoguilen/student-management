namespace StudentManagement.Api.IntegrationTest.Endpoints;

[Trait("Category", "Integration")]
public class AuthEndpointsTest(
    CustomWebApplicationFactory factory,
    ITestOutputHelper outputHelper)
    : IntegrationTest(factory, outputHelper)
{
    [Fact]
    public async Task GenerateAuthTokenAsync_WhenRequestIsNull_ReturnsSuccessResponse()
    {
        // Arrange
        var request = (AuthRequest?)null;

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/token", request);

        // Assert
        (await response.Content.ReadFromJsonAsync<AuthResponse>())
            .Should()
            .Match<AuthResponse>(r => r.AccessToken != null && r.ExpiresIn > DateTime.UtcNow);
    }

    [Fact]
    public async Task GenerateAuthTokenAsync_WhenRequestIsNotNull_ReturnsSuccessResponse()
    {
        // Arrange
        var studentId = await AddStudentInDatabaseAsync();
        var request = new AuthRequest { StudentId = studentId };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/token", request);

        // Assert
        (await response.Content.ReadFromJsonAsync<AuthResponse>())
            .Should()
            .Match<AuthResponse>(r => r.AccessToken != null && r.ExpiresIn > DateTime.UtcNow);
    }

    [Fact]
    public async Task GenerateAuthTokenAsync_WhenRequestContainsInvalidStudentId_ReturnsUnauthorized()
    {
        // Arrange
        var request = new AuthRequest { StudentId = Guid.NewGuid() };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/token", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
