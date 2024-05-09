namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a response to a grade request.
/// </summary>
public record GradeResponse
{
    /// <summary>
    /// The grade ID.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; init; }

    /// <summary>
    /// The student ID.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid StudentId { get; init; }

    /// <summary>
    /// The subject ID.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid SubjectId { get; init; }

    /// <summary>
    /// The grade value.
    /// </summary>
    /// <example>10</example>
    public int Value { get; init; }

    public static GradeResponse From(Grade grade) => new()
    {
        Id = grade.Id,
        StudentId = grade.StudentId,
        SubjectId = grade.SubjectId,
        Value = grade.Value
    };
}
