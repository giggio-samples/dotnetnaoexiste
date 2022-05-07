namespace NonEcsiste;

public class UsingNaoExiste
{
    static void UsingDispose()
    {
        var buffer = new byte[] { 1, 2, 3 };
        using (var ms = new MemoryStream())
        {
            ms.Write(buffer, 0, 3);
        }
    }

    static void UsingStatementDipose()
    {
        var buffer = new byte[] { 1, 2, 3 };
        using var ms = new MemoryStream();
        ms.Write(buffer, 0, 3);
    }
    async Task AwaitUsingDipose()
    {
        await using (var enumerator = CreateAsyncDispoableAsync())
        {
            while (await enumerator.MoveNextAsync())
            {
                WriteLine(enumerator.Current);
            }
        }
    }
    async Task AwaitUsingStatementDipose()
    {
        await using var enumerator = CreateAsyncDispoableAsync();
        while (await enumerator.MoveNextAsync())
            WriteLine(enumerator.Current);
    }
    IAsyncEnumerator<string> CreateAsyncDispoableAsync()
    {
        var stream = CreateStreamAsync();
        return stream.GetAsyncEnumerator();
    }
    async Task ReadStreamAsync()
    {
        WriteLine("um");
        await foreach (var line in CreateStreamAsync())
            WriteLine(line);
        WriteLine("dois");
    }

    static async IAsyncEnumerable<string> CreateStreamAsync()
    {
        using var sr = new StreamReader("path");
        while (!sr.EndOfStream)
            yield return await sr.ReadLineAsync();
    }
}
