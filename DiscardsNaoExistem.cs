namespace NonEcsiste;

public class DiscardsNaoExistem
{
    void DiscardReturn()
    {
        _ = RestornaString();
    }

    static string RestornaString() => "a";
    void DiscardOut()
    {
        Foo(out _);
    }
    void DiscardOut2()
    {
        int i = 2;
        Foo(out _);
    }

    static void Foo(out int i)
    {
        i = 1;
    }
}
