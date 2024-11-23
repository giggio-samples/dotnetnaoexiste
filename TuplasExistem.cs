namespace NonEcsiste;

public class TuplasExistem
{
    (int a, int b) Retorna()
    {
        var t = (a: 1, b: 2);
        return t;
    }

    public int Obtem1()
    {
        var (i, j) = Retorna();
        return i + j;
    }

    static (int c, int d) Retorna2()
    {
        var t = (c: 1, d: 2);
        return t;
    }

    public int Obtem2()
    {
        var t = Retorna2();
        var k = t.Item1;
        var l = t.Item2;
        return k + l;
    }
}
