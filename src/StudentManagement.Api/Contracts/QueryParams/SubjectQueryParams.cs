namespace StudentManagement.Api.Contracts.QueryParams;

/// <summary>
/// The query parameters to filter the subjects
/// </summary>
public record SubjectQueryParams
{
    /// <summary>
    /// The name of the subject
    /// </summary>
    /// <example>Math</example>
    public string? Name { get; init; }

    public static implicit operator SubjectFilter(SubjectQueryParams subjectQuery) => new(subjectQuery.Name);
}
