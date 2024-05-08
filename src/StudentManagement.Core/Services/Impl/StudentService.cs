namespace StudentManagement.Core.Services.Impl;

internal sealed class StudentService(IStudentRepository studentRepository) : IStudentService
{
    public async Task<StudentDto> CreateAsync(
        StudentDto student,
        CancellationToken cancellationToken = default)
    {
        var newStudent = await studentRepository.AddAsync((Student)student, cancellationToken);
        return (StudentDto)newStudent;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var deleted = await studentRepository.DeleteAsync(id, cancellationToken);
        return deleted > 0;
    }

    public async Task<(IEnumerable<StudentDto> Students, int TotalStudents)> GetAsync(
        StudentFilter studentFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default)
    {
        var (students, totalStudents) = await studentRepository.GetAllPaginatedAsync(
            studentFilter,
            paginationFilter,
            cancellationToken);
            
        return (students.Select(s => (StudentDto)s), totalStudents);
    }

    public async Task<StudentDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var student = await studentRepository.GetByIdAsync(id, cancellationToken);
        return student is null
            ? null
            : (StudentDto)student;
    }

    public async Task<StudentDto?> UpdateAsync(
        Guid id,
        StudentDto student,
        CancellationToken cancellationToken = default)
    {
        var existentStudent = await studentRepository.GetByIdAsync(id, cancellationToken);
        if (existentStudent is null)
        {
            return null;
        }

        existentStudent.Update(student);

        var updatedStudent = await studentRepository.UpdateAsync(existentStudent, cancellationToken);
        return (StudentDto)updatedStudent;
    }
}
