namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a student grade item response
/// </summary>
public record StudentGradeItemResponse
{
    /// <summary>
    /// The subject of grade
    /// </summary>
    public required SubjectResponse Subject { get; init; }

    /// <summary>
    /// The grade value
    /// </summary>
    /// <example>10</example>
    public required int Value { get; init; }

    public static StudentGradeItemResponse From(GradeDto grade) => new()
    {
        Subject = SubjectResponse.From(grade.Subject!),
        Value = grade.Value,
    };
}