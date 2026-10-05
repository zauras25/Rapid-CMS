using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryPageRepository : IPageRepository
{
    private readonly Dictionary<Guid, Page> _pages = new();

    public void Add(Page page)
    {
        _pages[page.Id.Value] = page;
    }

    public Task<Page?> GetByIdAsync(
        PageId pageId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _pages.TryGetValue(
            pageId.Value,
            out var page);

        return Task.FromResult(page);
    }
}
