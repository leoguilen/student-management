namespace StudentManagement.Infrastructure.Data.Repositories;

internal sealed class SubjectRepository(AppDbContext context) : ISubjectRepository
{
    public async Task<Subject> AddAsync(
        Subject entity,
        CancellationToken cancellationToken = default)
    {
        var newSubject = await context.Subjects.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return newSubject.Entity;
    }

    public async Task<int> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var subject = await context.Subjects.FindAsync([id], cancellationToken);
        if (subject is null)
        {
            return 0;
        }

        context.Subjects.Remove(subject);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public Task<IQueryable<Subject>> GetAllAsync(
        Expression<Func<Subject, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        var subjects = context.Subjects.AsNoTracking();
        if (predicate is not null)
        {
            subjects = subjects.Where(predicate);
        }

        subjects = subjects
            .Include(s => s.Grades)
            .ThenInclude(g => g.Student);

        return Task.FromResult(subjects);
    }

    public async Task<(IEnumerable<Subject> Subjects, int TotalSubjects)> GetAllPaginatedAsync(
        SubjectFilter subjectFilter,
        PaginationFilter paginationFilter,
        CancellationToken cancellationToken = default)
    {
        var subjects = context.Subjects.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(subjectFilter.Name))
        {
            subjects = subjects.Where(s => EF.Functions.Like(s.Name, $"%{subjectFilter.Name}%"));
        }

        var totalSubjects = await subjects.CountAsync(cancellationToken);

        return (subjects.Skip((paginationFilter.Page - 1) * paginationFilter.Size).Take(paginationFilter.Size), totalSubjects);
    }

    public async Task<Subject?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        => await context.Subjects
            .AsNoTracking()
            .Include(s => s.Grades)
            .ThenInclude(g => g.Student)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<(IQueryable<Subject>, int)> GetPagedAsync(
        Expression<Func<Subject, bool>> predicate,
        int page,
        int size,
        CancellationToken cancellationToken = default)
    {
        var subjects = context.Subjects.AsNoTracking().Where(predicate);
        var totalSubjects = await subjects.CountAsync(cancellationToken);
        return (subjects.Skip((page - 1) * size).Take(size), totalSubjects);
    }

    public async Task<Subject> UpdateAsync(
        Subject entity,
        CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        entry.CurrentValues.SetValues(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }
}
