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

    public static UnicodeCategories operator |(UnicodeCategories x, UnicodeCategories y) => new(x._flags | y._flags);

    public static readonly UnicodeCategories UppercaseLetter = new(1 << 0);
    public static readonly UnicodeCategories LowercaseLetter = new(1 << 1);
    public static readonly UnicodeCategories TitlecaseLetter = new(1 << 2);
    public static readonly UnicodeCategories ModifierLetter = new(1 << 3);
    public static readonly UnicodeCategories OtherLetter = new(1 << 4);

    public static readonly UnicodeCategories NonSpacingMark = new(1 << 5);
    public static readonly UnicodeCategories SpacingCombiningMark = new(1 << 6);
    public static readonly UnicodeCategories EnclosingMark = new(1 << 7);

    public static readonly UnicodeCategories DecimalDigitNumber = new(1 << 8);
    public static readonly UnicodeCategories LetterNumber = new(1 << 9);
    public static readonly UnicodeCategories OtherNumber = new(1 << 10);

    public static readonly UnicodeCategories SpaceSeparator = new(1 << 11);
    public static readonly UnicodeCategories LineSeparator = new(1 << 12);
    public static readonly UnicodeCategories ParagraphSeparator = new(1 << 13);

    public static readonly UnicodeCategories Control = new(1 << 14);
    public static readonly UnicodeCategories Format = new(1 << 15);
    public static readonly UnicodeCategories Surrogate = new(1 << 16);
    public static readonly UnicodeCategories PrivateUse = new(1 << 17);

    public static readonly UnicodeCategories ConnectorPunctuation = new(1 << 18);
    public static readonly UnicodeCategories DashPunctuation = new(1 << 19);
    public static readonly UnicodeCategories OpenPunctuation = new(1 << 20);
    public static readonly UnicodeCategories ClosePunctuation = new(1 << 21);
    public static readonly UnicodeCategories InitialQuotePunctuation = new(1 << 22);
    public static readonly UnicodeCategories FinalQuotePunctuation = new(1 << 23);
    public static readonly UnicodeCategories OtherPunctuation = new(1 << 24);

    public static readonly UnicodeCategories MathSymbol = new(1 << 25);
    public static readonly UnicodeCategories CurrencySymbol = new(1 << 26);
    public static readonly UnicodeCategories ModifierSymbol = new(1 << 27);
    public static readonly UnicodeCategories OtherSymbol = new(1 << 28);

    public static UnicodeCategories L = UppercaseLetter | LowercaseLetter | TitlecaseLetter | ModifierLetter | OtherLetter;
    public static UnicodeCategories M = NonSpacingMark| SpacingCombiningMark | EnclosingMark;
    public static UnicodeCategories N = DecimalDigitNumber | LetterNumber | OtherNumber;
    public static UnicodeCategories Z = SpaceSeparator | LineSeparator | ParagraphSeparator;
    public static UnicodeCategories C = Control | Format | Surrogate | PrivateUse;
    public static UnicodeCategories P = ConnectorPunctuation | DashPunctuation | OpenPunctuation | ClosePunctuation | InitialQuotePunctuation | FinalQuotePunctuation | OtherPunctuation;
    public static UnicodeCategories S = MathSymbol | CurrencySymbol | ModifierSymbol | OtherSymbol;
}
