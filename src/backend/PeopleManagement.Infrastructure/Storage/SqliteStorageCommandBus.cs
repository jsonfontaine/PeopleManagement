using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class SqliteStorageCommandBus : IStorageCommandBus
{
    private readonly IServiceProvider _serviceProvider;

    public SqliteStorageCommandBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> ExecuteAsync<TResult>(IStorageCommand<TResult> command, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IStorageCommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        dynamic typedCommand = command;
        TResult result = await handler.HandleAsync(typedCommand, cancellationToken);
        return result;
    }
}

