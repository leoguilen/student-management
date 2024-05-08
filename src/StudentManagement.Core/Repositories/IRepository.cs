namespace StudentManagement.Core.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
