using System;
using System.Collections.Generic;

namespace NonEcsiste
{
    public class YieldNaoExiste
    {

        public IEnumerable<int> Infinito()
        {
            int i = 100 + DateTime.Now.Second;
            yield return i;
            while (true)
            {
                Console.WriteLine(1);
                yield return i++;
                Console.WriteLine(2);
                yield return i++;
                Console.WriteLine(3);
            }
        }

    }
}
