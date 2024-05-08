namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a paged response.
/// </summary>
/// <typeparam name="T">The type of the items in the response.</typeparam>
public record PagedResponse<T>
{
    public PagedResponse(IEnumerable<T> items, int totalItems, int page, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    /// <summary>
    /// The items in the response.
    /// </summary>
    public IEnumerable<T> Items { get; init; }
    
    /// <summary>
    /// The current page.
    /// </summary>
    /// <example>1</example>
    public int Page { get; init; }
    
    /// <summary>
    /// The number of items per page.
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; init; }
    
    /// <summary>
    /// The total number of items.
    /// </summary>
    /// <example>100</example>
    public int TotalItems { get; init; }
    
    /// <summary>
    /// The total number of pages.
    /// </summary>
    /// <example>10</example>
    public int TotalPages { get; init; }
}
