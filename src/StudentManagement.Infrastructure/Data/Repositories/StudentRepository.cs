namespace StudentManagement.Infrastructure.Data.Repositories;

internal sealed class StudentRepository : IStudentRepository
{
    public Task<Student> AddAsync(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Student>> GetAllAsync(Expression<Func<Student, bool>> predicate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Student> UpdateAsync(Student entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
