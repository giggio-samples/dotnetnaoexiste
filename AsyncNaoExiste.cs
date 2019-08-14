using System;
using System.IO;
using System.Threading.Tasks;

namespace NonEcsiste
{
    public class AsyncNaoExiste
    {
        private string file;
        async Task FooAsync()
        {
            int n = GetInt();
            Console.WriteLine(n);
            await Task.Delay(100);
            int o = GetInt(n);
            await Task.Delay(200);
            int p = GetInt(o);
            Console.WriteLine(p);
            var text = await File.ReadAllTextAsync(file);
            Console.WriteLine(text);
        }

        private int GetInt(int i = 0) => DateTime.Now.Second + i;
    }
}
