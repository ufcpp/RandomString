using System.Globalization;

namespace RandomString;

public readonly struct UnicodeCategories
{
    private readonly int _flags;

    private UnicodeCategories(int flags) => _flags = flags;

    public static readonly UnicodeCategories All = new((1 << 28) - 1);

    public UnicodeCategories(params UnicodeCategory[] categories)
    {
        foreach (var cat in categories)
        {
            _flags |= 1 << (int)cat;
        }
    }

    public bool Contains(UnicodeCategory category) => (_flags & (1 << (int)category)) != 0;
}
