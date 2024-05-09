namespace StudentManagement.Core.Services;

public interface ISubjectService
{
    Task<(IEnumerable<SubjectDto> Subjects, int TotalSubjects)> GetAsync(
        SubjectFilter subjectFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);

    Task<SubjectDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SubjectDto> CreateAsync(SubjectDto subject, CancellationToken cancellationToken = default);

    Task<SubjectDto?> UpdateAsync(Guid id, SubjectDto subject, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
