namespace StudentManagement.Core.Repositories;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<(IEnumerable<Subject> Subjects, int TotalSubjects)> GetAllPaginatedAsync(
        SubjectFilter subjectFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);
}
