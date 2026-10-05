using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Application.Abstractions;

public interface IPageRepository
{
    Task<Page?> GetByIdAsync(
        PageId pageId,
        CancellationToken cancellationToken = default);
}
