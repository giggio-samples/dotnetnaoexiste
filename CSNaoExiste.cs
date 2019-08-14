using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NonEcsiste
{
    public class CSNaoExiste
    {

        void TC()
        {
            try
            {
                Console.WriteLine("Foo");
            }
            catch (NullReferenceException nre)
            {
                throw;
            }
        }

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

        async Task Async()
        {
            await Task.Delay(0);
            Console.WriteLine("dois");
        }

        IEnumerable<int> Enum()
        {
            Console.WriteLine("um");
            yield return 1;
            Console.WriteLine("dois");
            yield return 2;
            Console.WriteLine("tres");
        }

        async Task ReadStreamAsync()
        {
            Console.WriteLine("um");
            await foreach (var line in CreateStreamAsync())
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("dois");
        }
        async IAsyncEnumerable<string> CreateStreamAsync()
        {
            using var sr = new StreamReader("path");
            while (true)
            {
                yield return await sr.ReadLineAsync();
            }
        }

        IAsyncEnumerator<string> CreateAsyncDispoableAsync()
        {
            var stream = CreateStreamAsync();
            return stream.GetAsyncEnumerator();
        }

        async Task AwaitUsingDipose()
        {
            await using (var enumerator = CreateAsyncDispoableAsync())
            {
                while (await enumerator.MoveNextAsync())
                {
                    Console.WriteLine(enumerator.Current);
                }
            }
        }
        async Task AwaitUsingStatementDipose()
        {
            await using var enumerator = CreateAsyncDispoableAsync();
            while (await enumerator.MoveNextAsync())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
#nullable enable
        public string? ProduceNullableString() => DateTime.Now.Second > 30 ? null : "string";
        void TakesStringAndNullableString(string text, string? nullableString) => Console.WriteLine(text);

        void ConsumesNullableString1()
        {
            var nullableString = ProduceNullableString();
            if (nullableString == null)
                return;
            TakesStringAndNullableString(nullableString, null);
        }
        void ConsumesNullableString2()
        {
            var nullableString = ProduceNullableString();
            if (string.IsNullOrWhiteSpace(nullableString))
                return;
            TakesStringAndNullableString(nullableString, null);
        }
    }


}
