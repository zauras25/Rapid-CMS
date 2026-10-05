using RapidCMS.Contracts.Commands;

namespace RapidCMS.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand
{
    Task<CommandResult<TResult>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}
