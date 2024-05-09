namespace StudentManagement.Api.Contracts.Requests;

/// <summary>
/// Represents the request to generate an authentication token for a student.
/// </summary>
public record AuthRequest
{
    /// <summary>
    /// The identifier of the student.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid? StudentId { get; init; }
}
