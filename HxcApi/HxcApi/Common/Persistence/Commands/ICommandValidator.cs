namespace HxcApi.Common.Persistence.Commands;

public interface ICommandValidator<TCommand>
    where TCommand : ICommand
{
    Task ValidateAsync(TCommand command);
}