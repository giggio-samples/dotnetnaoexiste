#nullable enable
namespace NonEcsiste;

public class NullableNaoExiste
{
    public static string? ProduceNullableString() => DateTime.Now.Second > 30 ? null : "string";
    static void TakesStringAndNullableString(string text, string? nullableString) => WriteLine(text);

    void ConsumesNullableStringWithNullCheck()
    {
        var nullableString = ProduceNullableString();
        if (nullableString == null)
            return;
        TakesStringAndNullableString(nullableString, null);
    }
    void ConsumesNullableStringWithIsNullOrWhiteSpace()
    {
        var nullableString = ProduceNullableString();
        if (string.IsNullOrWhiteSpace(nullableString))
            return;
        TakesStringAndNullableString(nullableString, null);
    }
}


