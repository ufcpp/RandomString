using System.Text;

namespace RandomString;

public static class CharacterTableExtensions
{
    public static Rune GetRandomChar(this CharacterTable table, Random random)
    {
        var i = random.Next(0, table.BmpCharacters.Length + table.SmpCharacters.Length);

        if (i < table.BmpCharacters.Length) return new(table.BmpCharacters[i]);
        else return table.SmpCharacters[i - table.BmpCharacters.Length];
    }

    public static string GetRandomString(this CharacterTable table, int minLength, int maxLength, Random random)
    {
        var length = random.Next(maxLength - minLength + 1) + minLength;
        return GetRandomString(table, length, random);
    }

    public static string GetRandomString(this CharacterTable table, int length, Random random)
    {
        var buffer = (stackalloc char[2 * length]);
        var written = 0;

        for (int i = 0; i < length; i++)
        {
            var c = GetRandomChar(table, random);
            if (c.IsBmp) buffer[written++] = (char)c.Value;
            else
            {
                c.EncodeToUtf16(buffer[written..]);
                written += 2;
            }
        }

        return new(buffer[..written]);
    }
}
