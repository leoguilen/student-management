namespace StudentManagement.Api.Contracts.QueryParams;

/// <summary>
/// Query parameters for pagination
/// </summary>
public record PaginationQueryParams
{
    /// <summary>
    /// The page number
    /// </summary>
    /// <example>1</example>
    public int Page { get; init; } = 1;

    /// <summary>
    /// The number of items per page
    /// </summary>
    /// <example>10</example>
    public int Size { get; init; } = 10;

    /// <summary>
    /// The property to order by
    /// </summary>
    /// <example>Name</example>
    public string? OrderBy { get; init; }

    public static implicit operator PaginationFilter(PaginationQueryParams studentQueryParams) => new(
        studentQueryParams.Page,
        studentQueryParams.Size,
        studentQueryParams.OrderBy);
}
