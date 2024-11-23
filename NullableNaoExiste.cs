#nullable enable
namespace NonEcsiste;

public class NullableNaoExiste
{
    // ganha o atributo [NullableContext(2)]
    public static string? ProduceNullableString() => DateTime.Now.Second > 30 ? null : "string";
    // ganha o atributo [NullableContext(1)]
    static void TakesStringAndNullableString(string text, /* [Nullable(2)] */ string? nullableString) => WriteLine(text);

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


