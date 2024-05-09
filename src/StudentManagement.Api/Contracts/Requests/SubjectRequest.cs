namespace StudentManagement.Api.Contracts.Requests;

/// <summary>
/// The request for a subject
/// </summary>
public record SubjectRequest
{
    /// <summary>
    /// The name of the subject
    /// </summary>
    /// <example>Math</example>
    public required string Name { get; init; }

    public static implicit operator SubjectDto(SubjectRequest subjectRequest) => new()
    {
        Name = subjectRequest.Name
    };
}
