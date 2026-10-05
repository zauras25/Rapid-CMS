using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Abstractions;

public interface IPrototypeRepository
{
    Task<bool> ExistsAsync(
        PrototypeId prototypeId,
        CancellationToken cancellationToken = default);
}
