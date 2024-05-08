namespace StudentManagement.Api.Contracts.QueryParams;

/// <summary>
/// Query parameters for filtering students
/// </summary>
public record StudentQueryParams
{
    /// <summary>
    /// The name of the student
    /// </summary>
    /// <example>John Doe</example>
    public string? Name { get; init; }

    /// <summary>
    /// The date of birth of the student.
    /// </summary>
    /// <example>2000-01-01</example>
    public DateOnly? DateOfBirth { get; init; }

    /// <summary>
    /// The CPF of the student
    /// </summary>
    /// <example>123.456.789-00</example>
    public string? Cpf { get; init; }

    public static implicit operator StudentFilter(StudentQueryParams studentQueryParams) => new(
        studentQueryParams.Name,
        studentQueryParams.DateOfBirth,
        studentQueryParams.Cpf);
}
