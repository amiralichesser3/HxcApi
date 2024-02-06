namespace HxcApi.DataAccess.Contracts.Common.Commands;

public interface ICommandHandler<T> where T : ICommand
{
    Task HandleAsync(T command);
}