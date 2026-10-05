namespace RapidCMS.Domain.Identity;

public readonly record struct PageId(Guid Value)
{
    public PageId(int value)
        : this(CreateGuid(value))
    {
    }

    public static PageId New()
        => new(Guid.NewGuid());

    public static implicit operator PageId(int value)
        => new(value);

    public static implicit operator Guid(PageId id)
        => id.Value;

    public override string ToString()
        => Value.ToString();

    private static Guid CreateGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}
