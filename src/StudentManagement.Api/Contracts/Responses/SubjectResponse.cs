namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// The response for a subject
/// </summary>
public record SubjectResponse
{
    /// <summary>
    /// The ID of the subject
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public required Guid Id { get; init; }

    /// <summary>
    /// The name of the subject
    /// </summary>
    /// <example>Math</example>
    public required string Name { get; init; }

    public static SubjectResponse From(SubjectDto subject) => new()
    {
        Id = subject.Id,
        Name = subject.Name
    };
}
