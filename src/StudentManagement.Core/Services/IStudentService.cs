namespace StudentManagement.Core.Services;

public interface IStudentService
{
    Task<(IEnumerable<StudentDto> Students, int TotalStudents)> GetAsync(
        StudentFilter studentFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default);

    Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<StudentDto> CreateAsync(StudentDto student, CancellationToken cancellationToken = default);

    Task<StudentDto?> UpdateAsync(Guid id, StudentDto student, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<StudentGradeDto?> GetStudentGradesAsync(Guid id, CancellationToken cancellationToken = default);
}
