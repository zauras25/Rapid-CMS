using Microsoft.EntityFrameworkCore;

namespace RapidCMS.Infrastructure.Persistence;

public sealed class RapidCmsDbContext : DbContext
{
    public RapidCmsDbContext(
        DbContextOptions<RapidCmsDbContext> options)
        : base(options)
    {
    }

    public DbSet<DocumentRecord> Documents => Set<DocumentRecord>();
    public DbSet<DocumentPageRecord> DocumentPages => Set<DocumentPageRecord>();
    public DbSet<DocumentAssetRecord> DocumentAssets => Set<DocumentAssetRecord>();
    public DbSet<DocumentVariableRecord> DocumentVariables => Set<DocumentVariableRecord>();
    public DbSet<DocumentPrototypeRecord> DocumentPrototypes => Set<DocumentPrototypeRecord>();
    public DbSet<DocumentReferenceRecord> DocumentReferences => Set<DocumentReferenceRecord>();
    public DbSet<DocumentComponentRecord> DocumentComponents => Set<DocumentComponentRecord>();

    public DbSet<PageRecord> Pages => Set<PageRecord>();
    public DbSet<NodeRecord> Nodes => Set<NodeRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DocumentRecord>(entity =>
        {
            entity.ToTable("Documents");

            entity.HasKey(x => x.Id);

            entity.HasMany(x => x.Pages)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Assets)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Variables)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Prototypes)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.References)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Components)
                .WithOne()
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DocumentPageRecord>(entity =>
        {
            entity.ToTable("DocumentPages");
            entity.HasKey(x => new { x.DocumentId, x.PageId });
            entity.HasIndex(x => x.PageId);
        });

        modelBuilder.Entity<DocumentAssetRecord>(entity =>
        {
            entity.ToTable("DocumentAssets");
            entity.HasKey(x => new { x.DocumentId, x.AssetId });
            entity.HasIndex(x => x.AssetId);
        });

        modelBuilder.Entity<DocumentVariableRecord>(entity =>
        {
            entity.ToTable("DocumentVariables");
            entity.HasKey(x => new { x.DocumentId, x.VariableId });
            entity.HasIndex(x => x.VariableId);
        });

        modelBuilder.Entity<DocumentPrototypeRecord>(entity =>
        {
            entity.ToTable("DocumentPrototypes");
            entity.HasKey(x => new { x.DocumentId, x.PrototypeId });
            entity.HasIndex(x => x.PrototypeId);
        });

        modelBuilder.Entity<DocumentReferenceRecord>(entity =>
        {
            entity.ToTable("DocumentReferences");
            entity.HasKey(x => new { x.DocumentId, x.ReferenceId });
            entity.HasIndex(x => x.ReferenceId);
        });

        modelBuilder.Entity<DocumentComponentRecord>(entity =>
        {
            entity.ToTable("DocumentComponents");
            entity.HasKey(x => new { x.DocumentId, x.ComponentId });
            entity.HasIndex(x => x.ComponentId);
        });

        modelBuilder.Entity<PageRecord>(entity =>
        {
            entity.ToTable("Pages");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(x => x.DocumentId);

            entity.HasIndex(x => x.RootNodeId);
        });

        modelBuilder.Entity<NodeRecord>(entity =>
        {
            entity.ToTable("Nodes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(x => x.PageId);

            entity.HasIndex(x => x.ParentId);

            entity.HasIndex(x => new
            {
                x.PageId,
                x.ParentId,
                x.SortOrder
            });
        });
    }
}
