using RandomString;
using static RandomString.UnicodeCategories;
using static System.Text.Unicode.UnicodeRanges;

// https://en.wikipedia.org/wiki/Unicode_block
var table1 = CharacterTable.Create(
    (L | N, BasicLatin),
    (UppercaseLetter, LatinExtendedA),
    (OtherLetter | OtherPunctuation | ModifierLetter, Hiragana),
    (OtherLetter, new(0x20000, 10)), // CJK Unified Ideographs Extension B
    (OtherSymbol, new(0x1F600, 20)) // Emoticon
    );

Write(table1, 10, 3, 8);

var table2 = CharacterTable.Create(
    L,
    BasicLatin,
    Hiragana,
    Katakana
    );

Write(table2, 10, 3, 8);

var emoji = CharacterTable.Create(
    OtherSymbol,
    new RuneRange(0x1F000, 2800)
    );

Write(emoji, 10, 8, 8);

var ascii = CharacterTable.Create(
    L | N | P,
    BasicLatin);

Write(ascii, 10, 8, maxLength: 20);

static void Write(CharacterTable table, int num, int minLength, int maxLength)
{
    var random = new Random();

    for (int i = 0; i < num; i++)
    {
        Console.WriteLine(table.GetRandomString(minLength, maxLength, random));
    }
}

