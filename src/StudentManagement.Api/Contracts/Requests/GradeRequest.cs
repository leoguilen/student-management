namespace StudentManagement.Api.Contracts.Requests;

/// <summary>
/// Represents a request to create a new grade.
/// </summary>
public record GradeRequest
{
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

    public static implicit operator GradeDto(GradeRequest request) => new()
    {
        StudentId = request.StudentId,
        SubjectId = request.SubjectId,
        Value = request.Value
    };
}
