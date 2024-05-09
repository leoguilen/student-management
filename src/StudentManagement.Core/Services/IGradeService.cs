namespace StudentManagement.Core.Services;

public interface IGradeService
{
    Task<GradeDto> CreateAsync(GradeDto grade, CancellationToken cancellationToken = default);

    Task<GradeDto?> UpdateAsync(Guid id, GradeDto grade, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
