namespace RapidCMS.Infrastructure.Persistence;

public sealed class DocumentRecord
{
    public Guid Id { get; set; }

    public ICollection<DocumentPageRecord> Pages { get; set; }
        = new List<DocumentPageRecord>();

    public ICollection<DocumentAssetRecord> Assets { get; set; }
        = new List<DocumentAssetRecord>();

    public ICollection<DocumentVariableRecord> Variables { get; set; }
        = new List<DocumentVariableRecord>();

    public ICollection<DocumentPrototypeRecord> Prototypes { get; set; }
        = new List<DocumentPrototypeRecord>();

    public ICollection<DocumentReferenceRecord> References { get; set; }
        = new List<DocumentReferenceRecord>();

    public ICollection<DocumentComponentRecord> Components { get; set; }
        = new List<DocumentComponentRecord>();
}

public sealed class DocumentPageRecord
{
    public Guid DocumentId { get; set; }
    public Guid PageId { get; set; }
}

public sealed class DocumentAssetRecord
{
    public Guid DocumentId { get; set; }
    public Guid AssetId { get; set; }
}

public sealed class DocumentVariableRecord
{
    public Guid DocumentId { get; set; }
    public Guid VariableId { get; set; }
}

public sealed class DocumentPrototypeRecord
{
    public Guid DocumentId { get; set; }
    public Guid PrototypeId { get; set; }
}

public sealed class DocumentReferenceRecord
{
    public Guid DocumentId { get; set; }
    public Guid ReferenceId { get; set; }
}

public sealed class DocumentComponentRecord
{
    public Guid DocumentId { get; set; }
    public Guid ComponentId { get; set; }
}
