using RapidCMS.Contracts.Commands;

namespace RapidCMS.Application.Abstractions;

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<CommandResult> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}
