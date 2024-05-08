namespace StudentManagement.Core;

public interface IUnitOfWork
{
    IStudentRepository Students { get; }

    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
