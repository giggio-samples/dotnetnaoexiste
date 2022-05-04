namespace NonEcsiste.YieldDesempenho;

public class YieldDesempenho
{
    public static IEnumerable<int> Infinito(int start)
    {
        if (start < 0)
            throw new ArgumentException("Must be greater than 0.", nameof(start));
        while (true)
            yield return start++;
    }
}

public class YieldDesempenhoMelhor
{
    public IEnumerable<int> Infinito(int start)
    {
        if (start < 0)
            throw new ArgumentException("Must be greater than 0.", nameof(start));
        return InfinitoImpl(start);
    }
    private static IEnumerable<int> InfinitoImpl(int start)
    {
        while (true)
            yield return start++;
    }
}
