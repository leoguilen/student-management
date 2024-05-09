namespace StudentManagement.Core.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
