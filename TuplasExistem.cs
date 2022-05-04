namespace NonEcsiste;

public class TuplasExistem
{
    static (int, int) Retorna()
    {
        var t = (a: 1, b: 2);
        return t;
    }

    void Obtem1()
    {
        var (i, j) = Retorna();
    }

    static (int, int) Retorna2()
    {
        var t = (b: 1, d: 2);
        return t;
    }

    void Obtem2()
    {
        var t = Retorna2();
        var i = t.Item1;
        var j = t.Item2;
    }
}
