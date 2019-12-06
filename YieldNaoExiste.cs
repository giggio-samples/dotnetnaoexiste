using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NonEcsiste
{
    public class YieldNaoExiste
    {
        IEnumerable<int> Enum()
        {
            Console.WriteLine("um");
            yield return 1;
            Console.WriteLine("dois");
            yield return 2;
            Console.WriteLine("tres");
            yield return 3;
        }

        public IEnumerable<int> Infinito()
        {
            int i = 100 + DateTime.Now.Second;
            yield return i;
            while (true)
            {
                Console.WriteLine(1);
                yield return i++;
                Console.WriteLine(2);
            }
        }

        public async IAsyncEnumerable<int> InfinitoAsync()
        {
            int i = 100 + DateTime.Now.Second;
            yield return i;
            while (true)
            {
                Console.WriteLine(1);
                await Task.Delay(1);
                yield return i++;
                Console.WriteLine(2);
            }
        }

    }
}
