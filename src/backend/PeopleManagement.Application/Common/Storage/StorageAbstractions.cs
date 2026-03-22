namespace PeopleManagement.Application.Common.Storage;

public readonly record struct StorageUnit;

public interface IStorageCommand<TResult>
{
}

public interface IStorageCommandBus
{
    Task<TResult> ExecuteAsync<TResult>(IStorageCommand<TResult> command, CancellationToken cancellationToken);
}

public interface IStorageCommandHandler<in TCommand, TResult>
    where TCommand : IStorageCommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}

