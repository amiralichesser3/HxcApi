using HxcApi.DataAccess.Contracts.Common.Events;
using Microsoft.Data.SqlClient;

namespace HxcApi.DataAccess.Contracts.Common.Commands;

public abstract class EventDispatchingCommandHandler<T>(
    IServiceProvider serviceProvider)
    : ICommandHandler<T>
    where T : ICommand, IEvent
{
    protected readonly SqlConnection WriteSqlConnection = serviceProvider.GetKeyedService<SqlConnection>("WriteSqlConnection")!;
    private readonly ICommandValidator<T>? _validator = serviceProvider.GetService<ICommandValidator<T>>();
    private readonly IEventPublisher<T>? _eventPublisher = serviceProvider.GetService<IEventPublisher<T>>();
    public async Task BeforeHandleAsync(T command)
    {
        if (_validator != null) await _validator.ValidateAsync(command);
    }

    public async Task AfterHandleAsync(T command)
    {
        if (_eventPublisher != null)
        {
            command.PublishedDateTime = DateTimeOffset.Now;
            await _eventPublisher.PublishAsync(command);
        }
    }

    public async Task HandleAsync(T command)
    {
        await BeforeHandleAsync(command);
        await Handle(command);
        await AfterHandleAsync(command);
    }

    protected abstract Task Handle(T command);
}