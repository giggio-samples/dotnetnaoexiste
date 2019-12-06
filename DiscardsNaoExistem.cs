namespace NonEcsiste
{
    public class DiscardsNaoExistem
    {
        void DiscardReturn()
        {
            _ = RestornaString();
        }
        string RestornaString() => "a";
        void DiscardOut()
        {
            Foo(out _);
        }
        void DiscardOut2()
        {
            int i = 2;
            Foo(out _);
        }
        void Foo(out int i)
        {
            i = 1;
        }
    }
}
