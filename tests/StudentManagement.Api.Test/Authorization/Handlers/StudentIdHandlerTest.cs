namespace StudentManagement.Api.Test.Authorization.Handlers;

[Trait("Category", "Unit")]
public class StudentIdHandlerTest
{
    [Fact]
    public async Task HandleRequirementAsync_WithValidStudentId_Succeeds()
    {
        // Arrange
        var studentId = Guid.NewGuid().ToString();
        var requirement = new StudentIdRequirement();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(
            claims: [new Claim(ClaimTypes.NameIdentifier, studentId)],
            authenticationType: "Test"));
        var httpContext = new DefaultHttpContext();
        httpContext.Request.RouteValues.Add("id", studentId);
        var authorizationHandlerContext = new AuthorizationHandlerContext(
            [requirement],
            claimsPrincipal,
            httpContext);
        var sut = new StudentIdHandler();

        // Act
        await sut.HandleAsync(authorizationHandlerContext);

        // Assert
        authorizationHandlerContext.HasSucceeded.Should().BeTrue();
    }
}