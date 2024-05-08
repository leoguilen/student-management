namespace StudentManagement.Infrastructure.Data.UnitOfWork;

internal sealed class UnitOfWork : IUnitOfWork
{
    public IStudentRepository Students => throw new NotImplementedException();

    public Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
