using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Infrastructure.Persistence;
using RapidCMS.Infrastructure.Repositories;

namespace RapidCMS.Application.Tests.Infrastructure;

public sealed class EfDocumentRepositoryTests
{
    [Fact]
    public async Task Add_Get_Delete_RoundTripsDocumentThroughSqlite()
    {
        await using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<RapidCmsDbContext>()
            .UseSqlite(connection)
            .Options;

        await using (var dbContext = new RapidCmsDbContext(options))
        {
            await dbContext.Database.EnsureCreatedAsync();

            var document = Document.Create();

            var pageId = PageId.New();
            var assetId = AssetId.New();
            var variableId = VariableId.New();
            var prototypeId = PrototypeId.New();
            var referenceId = ReferenceId.New();
            var componentId = ComponentId.New();

            document.AddPage(pageId);
            document.AddAsset(assetId);
            document.AddVariable(variableId);
            document.AddPrototype(prototypeId);
            document.AddReference(referenceId);
            document.AddComponent(componentId);

            var repository = new EfDocumentRepository(dbContext);

            await repository.AddAsync(document);
        }

        // Use a completely new DbContext to prove the data survived
        // outside the original EF change tracker.
        await using (var dbContext = new RapidCmsDbContext(options))
        {
            var repository = new EfDocumentRepository(dbContext);

            var loaded = await repository.GetByIdAsync(
                new DocumentId(
                    (await dbContext.Documents
                        .AsNoTracking()
                        .Select(x => x.Id)
                        .SingleAsync())));

            Assert.NotNull(loaded);

            Assert.Single(loaded!.PageIds);
            Assert.Single(loaded.AssetIds);
            Assert.Single(loaded.VariableIds);
            Assert.Single(loaded.PrototypeIds);
            Assert.Single(loaded.ReferenceIds);
            Assert.Single(loaded.ComponentIds);
        }

        Guid documentId;

        await using (var dbContext = new RapidCmsDbContext(options))
        {
            documentId = await dbContext.Documents
                .AsNoTracking()
                .Select(x => x.Id)
                .SingleAsync();

            var repository = new EfDocumentRepository(dbContext);

            var deleted = await repository.DeleteAsync(
                new DocumentId(documentId));

            Assert.True(deleted);
        }

        await using (var dbContext = new RapidCmsDbContext(options))
        {
            var repository = new EfDocumentRepository(dbContext);

            var loaded = await repository.GetByIdAsync(
                new DocumentId(documentId));

            Assert.Null(loaded);

            Assert.Equal(0, await dbContext.DocumentPages.CountAsync());
            Assert.Equal(0, await dbContext.DocumentAssets.CountAsync());
            Assert.Equal(0, await dbContext.DocumentVariables.CountAsync());
            Assert.Equal(0, await dbContext.DocumentPrototypes.CountAsync());
            Assert.Equal(0, await dbContext.DocumentReferences.CountAsync());
            Assert.Equal(0, await dbContext.DocumentComponents.CountAsync());
        }
    }
}
