using System.Text;

namespace RandomString;

public readonly struct CharacterTable
{
    private readonly char[] _bmpChars;
    private readonly Rune[] _smpChars;

    public CharacterTable(char[] bmpChars, Rune[] smpChars)
    {
        _bmpChars = bmpChars;
        _smpChars = smpChars;
    }

    public ReadOnlySpan<char> BmpCharacters => _bmpChars;
    public ReadOnlySpan<Rune> SmpCharacters => _smpChars;

    public IEnumerator<Rune> GetEnumerator()
    {
        foreach (var c in _bmpChars) yield return new(c);
        foreach (var c in _smpChars) yield return c;
    }

    public static CharacterTable Create(UnicodeCategories categories, params RuneRange[] ranges)
    {
        var (bmpCount, smpCount) = GetCount(ranges);
        var bmpBuffer = (stackalloc char[bmpCount]);
        var smpBuffer = (stackalloc Rune[smpCount]);

        int bmpWrittern = 0, smpWrittern = 0;
        foreach (var r in ranges)
        {
            Append(categories, r, ref bmpWrittern, ref smpWrittern, bmpBuffer, smpBuffer);
        }

        return new(bmpBuffer[..bmpWrittern].ToArray(), smpBuffer[..smpWrittern].ToArray());
    }

    public static CharacterTable Create(params (UnicodeCategories categories, RuneRange range)[] items)
    {
        var (bmpCount, smpCount) = GetCount(items.Select(t => t.range));
        var bmpBuffer = (stackalloc char[bmpCount]);
        var smpBuffer = (stackalloc Rune[smpCount]);

        int bmpWrittern = 0, smpWrittern = 0;
        foreach (var (categories, r) in items)
        {
            Append(categories, r, ref bmpWrittern, ref smpWrittern, bmpBuffer, smpBuffer);
        }

        return new(bmpBuffer[..bmpWrittern].ToArray(), smpBuffer[..smpWrittern].ToArray());
    }

    private static void Append(UnicodeCategories categories, RuneRange r, ref int bmpCount, ref int smpCount, Span<char> bmpBuffer, Span<Rune> smpBuffer)
    {
        const int FirstSmp = 0x10000;

        for (int i = 0; i < r.Length; i++)
        {
            var c = r.FirstCodePoint + i;

            if (!Rune.IsValid(c)) continue;
            if (!categories.Contains(Rune.GetUnicodeCategory(new(c)))) continue;

            if (c >= FirstSmp) smpBuffer[smpCount++] = new(c);
            else bmpBuffer[bmpCount++] = (char)c;
        }
    }

    private static (int bmp, int smp) GetCount(RuneRange range)
    {
        const int FirstSmp = 0x10000;
        var firstInclusive = range.FirstCodePoint;
        var lastExclusive = range.FirstCodePoint + range.Length;

        if (lastExclusive < FirstSmp) return (range.Length, 0);
        if (firstInclusive >= FirstSmp) return (0, range.Length);
        return (FirstSmp - firstInclusive, lastExclusive - FirstSmp);
    }

    private static (int bmp, int smp) GetCount(params RuneRange[] ranges) => GetCount(ranges.AsEnumerable());

    private static (int bmp, int smp) GetCount(IEnumerable<RuneRange> ranges)
    {
        var bmpCount = 0;
        var smpCount = 0;

        foreach (var r in ranges)
        {
            var (b, s) = GetCount(r);
            bmpCount += b;
            smpCount += s;
        }

        return (bmpCount, smpCount);
    }
}
