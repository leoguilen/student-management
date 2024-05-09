namespace StudentManagement.Api.Test.Controllers;

[Trait("Category", "Unit")]
public class GradesControllerTest
{
    private readonly Mock<IGradeService> _gradeServiceMock;

    public GradesControllerTest()
    {
        _gradeServiceMock = new(MockBehavior.Strict);
    }

    [Fact]
    public async Task CreateAsync_WhenRequestIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var request = new GradeRequest();
        var sut = new GradesController(_gradeServiceMock.Object);
        sut.ModelState.AddModelError("Grade", "Grade is required");

        // Act
        var result = await sut.CreateAsync(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task CreateAsync_WhenRequestIsValid_ReturnsCreated()
    {
        // Arrange
        var request = new GradeRequest { StudentId = Guid.NewGuid() };
        var grade = new Grade { StudentId = request.StudentId };
        _gradeServiceMock
            .Setup(x => x.CreateAsync(request, default))
            .ReturnsAsync(grade);
        var sut = new GradesController(_gradeServiceMock.Object);

        // Act
        var result = await sut.CreateAsync(request);

        // Assert
        result.Should().BeOfType<CreatedAtRouteResult>();
    }

    [Fact]
    public async Task UpdateAsync_WhenRequestIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new GradeRequest();
        var sut = new GradesController(_gradeServiceMock.Object);
        sut.ModelState.AddModelError("Grade", "Grade is required");

        // Act
        var result = await sut.UpdateAsync(id, request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task UpdateAsync_WhenRequestContainsInexistentId_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new GradeRequest { StudentId = Guid.NewGuid() };
        _gradeServiceMock
            .Setup(x => x.UpdateAsync(id, request, default))
            .ReturnsAsync((GradeDto?)null);
        var sut = new GradesController(_gradeServiceMock.Object);

        // Act
        var result = await sut.UpdateAsync(id, request);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task UpdateAsync_WhenRequestIsValid_ReturnsOk()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new GradeRequest { StudentId = Guid.NewGuid() };
        var grade = new Grade { StudentId = request.StudentId };
        _gradeServiceMock
            .Setup(x => x.UpdateAsync(id, request, default))
            .ReturnsAsync(grade);
        var sut = new GradesController(_gradeServiceMock.Object);

        // Act
        var result = await sut.UpdateAsync(id, request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task DeleteAsync_WhenGradeExists_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        _gradeServiceMock
            .Setup(x => x.DeleteAsync(id, default))
            .ReturnsAsync(true);
        var sut = new GradesController(_gradeServiceMock.Object);

        // Act
        var result = await sut.DeleteAsync(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteAsync_WhenGradeDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _gradeServiceMock
            .Setup(x => x.DeleteAsync(id, default))
            .ReturnsAsync(false);
        var sut = new GradesController(_gradeServiceMock.Object);

        // Act
        var result = await sut.DeleteAsync(id);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}