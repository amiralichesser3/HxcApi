namespace HxcApi.DataAccess.Contracts.Common.Commands;

public interface ICommandValidator<TCommand>
    where TCommand : ICommand
{
    Task ValidateAsync(TCommand command);
}