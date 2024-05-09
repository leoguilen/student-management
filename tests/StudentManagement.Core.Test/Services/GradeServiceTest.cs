namespace StudentManagement.Core.Test.Services;

[Trait("Category", "Unit")]
public class GradeServiceTest
{
    private static readonly GradeDtoFixture _gradeDtoFixture = new();

    private readonly Mock<IGradeRepository> _gradeRepositoryMock;

    public GradeServiceTest()
    {
        _gradeRepositoryMock = new(MockBehavior.Strict);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddGradeToRepository()
    {
        // Arrange
        var gradeDto = _gradeDtoFixture.Generate();
        _gradeRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Grade>(), default))
            .ReturnsAsync((Grade)gradeDto);
        var expectedResult = (GradeDto)(Grade)gradeDto;
        var sut = new GradeService(_gradeRepositoryMock.Object);

        // Act
        var result = await sut.CreateAsync(gradeDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteGradeFromRepository()
    {
        // Arrange
        var gradeId = Guid.NewGuid();
        _gradeRepositoryMock
            .Setup(repo => repo.DeleteAsync(gradeId, default))
            .ReturnsAsync(1);
        var sut = new GradeService(_gradeRepositoryMock.Object);

        // Act
        var result = await sut.DeleteAsync(gradeId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_WhenGradeDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var gradeId = Guid.NewGuid();
        _gradeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(gradeId, default))
            .ReturnsAsync((Grade?)null);
        var sut = new GradeService(_gradeRepositoryMock.Object);

        // Act
        var result = await sut.UpdateAsync(gradeId, _gradeDtoFixture.Generate());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_WhenGradeExists_ShouldUpdateGradeInRepository()
    {
        // Arrange
        var gradeDto = _gradeDtoFixture.Generate();
        var grade = (Grade)gradeDto;
        _gradeRepositoryMock
            .Setup(repo => repo.GetByIdAsync(gradeDto.Id, default))
            .ReturnsAsync(grade);
        _gradeRepositoryMock
            .Setup(repo => repo.UpdateAsync(grade, default))
            .ReturnsAsync(grade);
        var expectedResult = (GradeDto)grade;
        var sut = new GradeService(_gradeRepositoryMock.Object);

        // Act
        var result = await sut.UpdateAsync(gradeDto.Id, gradeDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}