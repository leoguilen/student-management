using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace StudentManagement.Api.IntegrationTest.Endpoints;

[Trait("Category", "Integration")]
public class StudentsEndpointsTest(
    CustomWebApplicationFactory factory,
    ITestOutputHelper outputHelper)
    : IntegrationTest(factory, outputHelper)
{
    [Fact]
    public async Task GetStudents_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange

        // Act
        var response = await Client.GetAsync("api/students");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetStudents_WhenAuthenticated_ShouldReturnStudents()
    {
        // Arrange
        var studentId = await AddStudentInDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var response = await Client.GetAsync("api/students");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadFromJsonAsync<PagedResponse<StudentResponse>>())
            .Should()
            .Match<PagedResponse<StudentResponse>>(r => r.Items.Any(s => s.Id == studentId));
    }

    [Fact]
    public async Task GetStudentById_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var studentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetStudentById_WhenAuthenticated_ShouldReturnStudent()
    {
        // Arrange
        var studentId = await AddStudentInDatabaseAsync();
        await AuthenticateAsync();

        // Act
        var response = await Client.GetAsync($"api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadFromJsonAsync<StudentResponse>())
            .Should()
            .Match<StudentResponse>(r => r.Id == studentId);
    }

    [Fact]
    public async Task CreateStudent_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = StudentRequestFixture.Generate();

        // Act
        var response = await Client.PostAsJsonAsync("api/students", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateStudent_WhenRequestIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsync();
        var invalidRequest = StudentRequestFixture
            .RuleFor(x => x.Name, f => string.Empty)
            .Generate();

        // Act
        var response = await Client.PostAsJsonAsync("api/students", invalidRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>())
            .Should()
            .Match<HttpValidationProblemDetails>(r => r.Errors.Any());
    }

    [Fact]
    public async Task CreateStudent_WhenAuthenticatedAndRequestIsValid_ShouldReturnCreated()
    {
        // Arrange
        await AuthenticateAsync();
        var request = StudentRequestFixture.Generate();

        // Act
        var response = await Client.PostAsJsonAsync("api/students", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        (await Client.GetAsync($"api/students/{response.Headers.Location!.PathAndQuery.Split('/').Last()}"))
            .StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateStudent_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var request = StudentRequestFixture.Generate();

        // Act
        var response = await Client.PutAsJsonAsync($"api/students/{studentId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateStudent_WhenRequestIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        await AuthenticateAsync();
        var studentId = Guid.NewGuid();
        var invalidRequest = StudentRequestFixture.Generate() with
        {
            Name = string.Empty
        };

        // Act
        var response = await Client.PutAsJsonAsync($"api/students/{studentId}", invalidRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        (await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>())
            .Should()
            .Match<HttpValidationProblemDetails>(r => r.Errors.Any());
    }

    [Fact]
    public async Task UpdateStudent_WhenUserDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        await AuthenticateAsync();
        var studentId = Guid.NewGuid();
        var request = StudentRequestFixture.Generate();

        // Act
        var response = await Client.PutAsJsonAsync($"api/students/{studentId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateStudent_WhenAuthenticatedAndRequestIsValid_ShouldReturnOk()
    {
        // Arrange
        await AuthenticateAsync();
        var studentId = await AddStudentInDatabaseAsync();
        var request = StudentRequestFixture.Generate();

        // Act
        var response = await Client.PutAsJsonAsync($"api/students/{studentId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteStudent_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var studentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteStudent_WhenUserDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        await AuthenticateAsync();
        var studentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteStudent_WhenAuthenticatedAndUserExists_ShouldReturnNoContent()
    {
        // Arrange
        await AuthenticateAsync();
        var studentId = await AddStudentInDatabaseAsync();

        // Act
        var response = await Client.DeleteAsync($"api/students/{studentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        (await Client.GetAsync($"api/students/{studentId}"))
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetGrades_WhenNotAuthenticated_ShouldReturnUnauthorized()
    {
        // Arrange
        var studentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"api/students/{studentId}/grades");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetGrades_WhenNotAuthorized_ShouldReturnForbidden()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        await AuthenticateAsync();

        // Act
        var response = await Client.GetAsync($"api/students/{studentId}/grades");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetGrades_WhenAuthenticatedAndAuthorized_ShouldReturnGrades()
    {
        // Arrange
        var studentId = await AddStudentInDatabaseAsync();
        await AuthenticateAsync(studentId);

        // Act
        var response = await Client.GetAsync($"api/students/{studentId}/grades");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadFromJsonAsync<StudentGradesResponse>())
            .Should()
            .Match<StudentGradesResponse>(r => r.Id == studentId);
    }
}
