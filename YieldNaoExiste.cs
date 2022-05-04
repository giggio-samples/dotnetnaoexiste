namespace NonEcsiste;

public class YieldNaoExiste
{
    static IEnumerable<int> Enum()
    {
        WriteLine("um");
        yield return 1;
        WriteLine("dois");
        yield return 2;
        WriteLine("tres");
        yield return 3;
    }

    public static IEnumerable<int> Infinito()
    {
        int i = 100 + DateTime.Now.Second;
        yield return i;
        while (true)
        {
            WriteLine(1);
            yield return i++;
            WriteLine(2);
        }
    }

    public static async IAsyncEnumerable<int> InfinitoAsync()
    {
        int i = 100 + DateTime.Now.Second;
        yield return i;
        while (true)
        {
            WriteLine(1);
            await Task.Delay(1);
            yield return i++;
            WriteLine(2);
        }
    }

}
