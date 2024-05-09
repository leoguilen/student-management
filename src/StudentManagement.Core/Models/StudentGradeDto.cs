namespace StudentManagement.Core.Models;

public record StudentGradeDto
{
    public required StudentDto Student { get; init; }

    public required IEnumerable<GradeDto> Grades { get; init; }
}
