namespace StudentManagement.Core.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<(IEnumerable<Student> Students, int TotalStudents)> GetAllPaginatedAsync(
        StudentFilter studentFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);
}
