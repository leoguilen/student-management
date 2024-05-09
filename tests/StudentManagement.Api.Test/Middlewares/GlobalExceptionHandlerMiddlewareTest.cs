namespace StudentManagement.Api.Test.Middlewares;

[Trait("Category", "Unit")]
public class GlobalExceptionHandlerMiddlewareTest
{
    private readonly Mock<IProblemDetailsService> _problemDetailsServiceMock;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _loggerStub;

    public GlobalExceptionHandlerMiddlewareTest()
    {
        _problemDetailsServiceMock = new(MockBehavior.Strict);
        _loggerStub = new NullLogger<GlobalExceptionHandlerMiddleware>();
    }

    [Fact]
    public async Task TryHandleAsync_WhenExceptionOccurs_ReturnsTrue()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var exception = new Exception("Test exception");
        var cancellationToken = CancellationToken.None;
        _problemDetailsServiceMock
            .Setup(x => x.TryWriteAsync(It.IsAny<ProblemDetailsContext>()))
            .ReturnsAsync(true);
        var sut = new GlobalExceptionHandlerMiddleware(_problemDetailsServiceMock.Object, _loggerStub);

        // Act
        var result = await sut.TryHandleAsync(httpContext, exception, cancellationToken);

        // Assert
        result.Should().BeTrue();
    }
}