using System;
using System.Threading.Tasks;

namespace NonEcsiste
{
    public class AsyncDesempenho
    {
        public async Task DoAsync(int start)
        {
            if (start < 0)
                throw new ArgumentException("Must be greater than 0.", nameof(start));
            await Task.Delay(100);
            Console.WriteLine(start);
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
        public async Task DoAsyncImpl(int start)
        {
            await Task.Delay(100);
            Console.WriteLine(start);
        }
    }
}
