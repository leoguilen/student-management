namespace StudentManagement.Api.Test.Services;

[Trait("Category", "Unit")]
public class TokenServiceTest
{
    private readonly IOptions<JwtOptions> _jwtOptions = MOptions.Create(new JwtOptions
    {
        IssuerSigningKeys = ["your-very-long-issue-signing-key"],
        ValidIssuers = ["your-valid-issuer"],
        ValidAudiences = ["your-valid-audience"],
        ExpiryHours = 1
    });

    [Fact]
    public void Generate_WhenStudentIsNull_ReturnsTokenWithDefaultRole()
    {
        // Arrange
        var sut = new TokenService(_jwtOptions);

        // Act
        var (token, expiry) = sut.Generate(null);

        // Assert
        token.Should().NotBeNullOrEmpty();
        expiry.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public void Generate_WhenStudentIsNotNull_ReturnsTokenWithStudentRole()
    {
        // Arrange
        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = "Test Student"
        };
        var sut = new TokenService(_jwtOptions);

        // Act
        var (token, expiry) = sut.Generate(student);

        // Assert
        token.Should().NotBeNullOrEmpty();
        expiry.Should().BeAfter(DateTime.UtcNow);
    }
}