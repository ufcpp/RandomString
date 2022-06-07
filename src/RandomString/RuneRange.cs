using System.Text.Unicode;

namespace RandomString;

public readonly struct RuneRange
{
    public readonly int FirstCodePoint;
    public readonly int Length;

    public RuneRange(int firstCodePoint, int length)
    {
        FirstCodePoint = firstCodePoint;
        Length = length;
    }

    public static implicit operator RuneRange(UnicodeRange r) => new(r.FirstCodePoint, r.Length);
}
