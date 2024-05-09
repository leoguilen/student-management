namespace StudentManagement.Infrastructure.Data.Repositories;

internal sealed class GradeRepository(AppDbContext context) : IGradeRepository
{
    public async Task<Grade> AddAsync(
        Grade entity,
        CancellationToken cancellationToken = default)
    {
        var newGrade = await context.Grades.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return newGrade.Entity;
    }

    public async Task<int> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var grade = await context.Grades.FindAsync([id], cancellationToken);
        if (grade is null)
        {
            return 0;
        }

        context.Grades.Remove(grade);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public Task<IQueryable<Grade>> GetAllAsync(
        Expression<Func<Grade, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        var grades = context.Grades.AsNoTracking();
        if (predicate is not null)
        {
            grades = grades.Where(predicate);
        }

        grades = grades
            .Include(s => s.Student)
            .Include(g => g.Subject);
            
        return Task.FromResult(grades);
    }

    public async Task<Grade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Grades
            .AsNoTracking()
            .Include(g => g.Student)
            .Include(g => g.Subject)
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

    public async Task<Grade> UpdateAsync(Grade entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        entry.CurrentValues.SetValues(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity;
    }
}
