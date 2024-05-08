namespace StudentManagement.Core.Models;

public record PaginationFilter(int Page, int Size, string? OrderBy);
