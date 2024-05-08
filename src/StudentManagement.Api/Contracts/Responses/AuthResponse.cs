namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents the response of a successful authentication token generation.
/// </summary>
public record AuthResponse
{
    /// <summary>
    /// The authentication token.
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</example>
    public required string AccessToken { get; init; }

    /// <summary>
    /// The expiration date of the token.
    /// </summary>
    /// <example>2022-12-31T23:59:59</example>
    public required DateTime ExpiresIn { get; init; }

    /// <summary>
    /// The type of the token.
    /// </summary>
    /// <example>Bearer</example>
    public string TokenType { get; init; } = "Bearer";
}
