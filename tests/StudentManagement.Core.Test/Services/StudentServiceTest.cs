namespace StudentManagement.Core.Test.Services;

[Trait("Category", "Unit")]
public class StudentServiceTest
{
    private static readonly StudentDtoFixture _studentDtoFixture = new();
    private static readonly StudentFixture _studentFixture = new();

    private readonly Mock<IStudentRepository> _mockStudentRepository;

    public StudentServiceTest()
    {
        _mockStudentRepository = new(MockBehavior.Strict);
    }

    [Fact]
    public async Task CreateAsync_ValidStudent_ReturnsCreatedStudent()
    {
        // Arrange
        var studentDto = _studentDtoFixture.Generate();
        var newStudent = (Student)studentDto;
        _mockStudentRepository
            .Setup(r => r.AddAsync(It.IsAny<Student>(), default))
            .ReturnsAsync(newStudent);
        var expectedResult = (StudentDto)newStudent;
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.CreateAsync(studentDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task DeleteAsync_ExistingStudent_ReturnsTrue()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        _mockStudentRepository
            .Setup(r => r.DeleteAsync(studentId, default))
            .ReturnsAsync(1);
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.DeleteAsync(studentId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GetAsync_ReturnsStudentsAndTotalStudents()
    {
        // Arrange
        var studentFilter = new StudentFilter(null, null, null);
        var paginationFilter = new PaginationFilter(1, 10, string.Empty);
        var students = _studentFixture.Generate(3);
        _mockStudentRepository
            .Setup(r => r.GetAllPaginatedAsync(studentFilter, paginationFilter, default))
            .ReturnsAsync((students, students.Count));
        var expectedStudents = (students.Select(s => (StudentDto)s), students.Count);
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.GetAsync(studentFilter, paginationFilter);

        // Assert
        result.Should().BeEquivalentTo(expectedStudents);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingStudent_ReturnsStudentDto()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var student = _studentFixture.Generate();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync(student);
        var expectedResult = (StudentDto)student;
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.GetByIdAsync(studentId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingStudent_ReturnsNull()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync((Student?)null);
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.GetByIdAsync(studentId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetStudentGradesAsync_NonExistingStudent_ReturnsNull()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync((Student?)null);
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.GetStudentGradesAsync(studentId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetStudentGradesAsync_ExistingStudent_ReturnsStudentGradeDto()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var student = _studentFixture.Generate();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync(student);
        var expectedResult = new StudentGradeDto
        {
            Student = (StudentDto)student,
            Grades = student.Grades.Select(g => (GradeDto)g)
        };
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.GetStudentGradesAsync(studentId);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateAsync_ExistingStudent_ReturnsUpdatedStudent()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var studentDto = _studentDtoFixture.Generate();
        var existingStudent = _studentFixture.Generate();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync(existingStudent);
        existingStudent.Update((Student)studentDto);
        _mockStudentRepository
            .Setup(r => r.UpdateAsync(existingStudent, default))
            .ReturnsAsync(existingStudent);
        var expectedResult = (StudentDto)existingStudent;
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.UpdateAsync(studentId, studentDto);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingStudent_ReturnsNull()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var studentDto = _studentDtoFixture.Generate();
        _mockStudentRepository
            .Setup(r => r.GetByIdAsync(studentId, default))
            .ReturnsAsync((Student?)null);
        var sut = new StudentService(_mockStudentRepository.Object);

        // Act
        var result = await sut.UpdateAsync(studentId, studentDto);

        // Assert
        result.Should().BeNull();
    }
}