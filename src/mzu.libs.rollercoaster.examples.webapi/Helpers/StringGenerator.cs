using System.Text;

namespace mzu.libs.rollercoaster.examples.webapi.Helpers;

public class StringGenerator
{
    private static readonly Random random = new Random();

    public static string GenerateMeaningfulString(int length)
    {
        const string vowels = "aeiou";
        const string consonants = "bcdfghjklmnpqrstvwxyz";
        const string punctuation = "!?.";
        const string whitespace = " ";

        StringBuilder sb = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            string characterSet = random.Next(2) == 0 ? vowels : consonants;

            characterSet += random.Next(3) == 0 ? punctuation : whitespace;

            char randomChar = characterSet[random.Next(characterSet.Length)];

            sb.Append(randomChar);
        }

        return sb.ToString();
    }
}
