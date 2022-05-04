namespace NonEcsiste.AsyncDesempenho;

public class AsyncDesempenho
{
    public static async Task DoAsync(int start)
    {
        if (start < 0)
            throw new ArgumentException("Must be greater than 0.", nameof(start));
        await Task.Delay(100);
        WriteLine(start);
    }
}

public class AsyncDesempenhoMelhor
{
    public Task DoAsync(int start)
    {
        if (start < 0)
            throw new ArgumentException("Must be greater than 0.", nameof(start));
        return DoAsyncImpl(start);
    }
    public static async Task DoAsyncImpl(int start)
    {
        await Task.Delay(100);
        WriteLine(start);
    }

    public static Task FooAsync(int start)
    {
        return Task.Delay(100);
    }
}
