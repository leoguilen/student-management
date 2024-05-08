namespace StudentManagement.Infrastructure.Data.Repositories;

internal sealed class StudentRepository(AppDbContext context) : IStudentRepository
{
    public async Task<Student> AddAsync(
        Student entity,
        CancellationToken cancellationToken = default)
    {
        var newStudent = await context.Students.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return newStudent.Entity;
    }

    public async Task<int> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var student = await context.Students.FindAsync([id], cancellationToken);
        if (student is null)
        {
            return 0;
        }

        context.Students.Remove(student);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public Task<IQueryable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(context.Students.AsNoTracking());

    public async Task<(IEnumerable<Student> Students, int TotalStudents)> GetAllPaginatedAsync(
        StudentFilter studentFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default)
    {
        var students = context.Students.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(studentFilter.Name))
        {
            students = students.Where(s => EF.Functions.Like(s.Name, $"%{studentFilter.Name}%"));
        }

        if (studentFilter.DateOfBirth.HasValue)
        {
            students = students.Where(s => s.DateOfBirth == studentFilter.DateOfBirth);
        }

        if (!string.IsNullOrWhiteSpace(studentFilter.Cpf))
        {
            students = students.Where(s => s.Cpf == studentFilter.Cpf);
        }
        
        var totalStudents = await students.CountAsync(cancellationToken);

        return (students.Skip((paginationFilter.Page - 1) * paginationFilter.Size).Take(paginationFilter.Size), totalStudents);
    }

    public async Task<Student?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await context.Students.FindAsync([id], cancellationToken);

    public async Task<(IQueryable<Student>, int)> GetPagedAsync(
        Expression<Func<Student, bool>> predicate,
        int page,
        int size,
        CancellationToken cancellationToken = default)
    {
        var students = context.Students.AsNoTracking().Where(predicate);
        var totalStudents = await students.CountAsync(cancellationToken);
        return (students.Skip((page - 1) * size).Take(size), totalStudents);
    }

    public async Task<Student> UpdateAsync(
        Student entity,
        CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        entry.CurrentValues.SetValues(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }
}
