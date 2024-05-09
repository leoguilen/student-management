namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a student grades response
/// </summary>
public record StudentGradesResponse
{
    /// <summary>
    /// The student ID
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public required Guid Id { get; init; }

    /// <summary>
    /// The student name
    /// </summary>
    /// <example>John Doe</example>
    public required string Name { get; init; }

    /// <summary>
    /// The student grades
    /// </summary>
    public IEnumerable<StudentGradeItemResponse>? Grades { get; init; }

    public static StudentGradesResponse From(StudentGradeDto studentGrade) => new()
    {
        Id = studentGrade.Student.Id,
        Name = studentGrade.Student.Name,
        Grades = studentGrade.Grades.Select(StudentGradeItemResponse.From),
    };
}
