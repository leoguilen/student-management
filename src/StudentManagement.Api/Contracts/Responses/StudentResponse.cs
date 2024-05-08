namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a student response schema.
/// </summary>
public record StudentResponse
{
    /// <summary>
    /// The ID of the student.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public Guid Id { get; init; }
    
    /// <summary>
    /// The name of the student.
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; init; }
}
