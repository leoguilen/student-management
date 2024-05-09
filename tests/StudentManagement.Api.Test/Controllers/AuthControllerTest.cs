namespace StudentManagement.Api.Test.Controllers;

[Trait("Category", "Unit")]
public class AuthControllerTest
{
    private readonly Mock<IStudentRepository> _studentRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly ILogger<AuthController> _loggerStub = new NullLogger<AuthController>();

    public AuthControllerTest()
    {
        _studentRepositoryMock = new(MockBehavior.Strict);
        _tokenServiceMock = new(MockBehavior.Strict);
    }

    [Fact]
    public async Task GenerateAuthTokenAsync_WhenRequestIsNull_ReturnsSuccessResponse()
    {
        // Arrange
        var request = (AuthRequest?)null;
        _tokenServiceMock
            .Setup(x => x.Generate(null))
            .Returns(("token", DateTime.UtcNow.AddHours(1).Date));
        var expectedResult = new OkObjectResult(new AuthResponse
        {
            AccessToken = "token",
            ExpiresIn = DateTime.UtcNow.AddHours(1).Date,
        });
        var sut = new AuthController(_studentRepositoryMock.Object, _tokenServiceMock.Object, _loggerStub);

        // Act
        var result = await sut.GenerateAuthTokenAsync(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

        [Fact]
    public async Task GenerateAuthTokenAsync_WhenRequestIsNotNull_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new AuthRequest { StudentId = Guid.NewGuid() };
        _studentRepositoryMock
            .Setup(x => x.GetByIdAsync(request.StudentId!.Value, default))
            .ReturnsAsync(new Student { Id = request.StudentId!.Value });
        _tokenServiceMock
            .Setup(x => x.Generate(It.Is<Student>(s => s.Id == request.StudentId!.Value)))
            .Returns(("token", DateTime.UtcNow.AddHours(1).Date));
        var expectedResult = new OkObjectResult(new AuthResponse
        {
            AccessToken = "token",
            ExpiresIn = DateTime.UtcNow.AddHours(1).Date,
        });
        var sut = new AuthController(_studentRepositoryMock.Object, _tokenServiceMock.Object, _loggerStub);

        // Act
        var result = await sut.GenerateAuthTokenAsync(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}