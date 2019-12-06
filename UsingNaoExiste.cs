using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Console;

namespace NonEcsiste
{
    public class UsingNaoExiste
    {
        void UsingDispose()
        {
            var buffer = new byte[] { 1, 2, 3 };
            using (var ms = new MemoryStream())
            {
                ms.Write(buffer, 0, 3);
            }
        }
        void UsingStatementDipose()
        {
            var buffer = new byte[] { 1, 2, 3 };
            using var ms = new MemoryStream();
            ms.Write(buffer, 0, 3);
        }
        async Task AwaitUsingDipose()
        {
            await using (var enumerator = CreateAsyncDispoableAsync())
                while (await enumerator.MoveNextAsync())
                    WriteLine(enumerator.Current);
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
            Console.WriteLine("um");
            await foreach (var line in CreateStreamAsync())
                Console.WriteLine(line);
            Console.WriteLine("dois");
        }
        async IAsyncEnumerable<string> CreateStreamAsync()
        {
            using var sr = new StreamReader("path");
            while (!sr.EndOfStream)
                yield return await sr.ReadLineAsync();
        }
    }
}
