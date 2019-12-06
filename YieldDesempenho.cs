using System;
using System.Collections.Generic;
using System.Text;

namespace NonEcsiste
{
    public class YieldDesempenho
    {
        public IEnumerable<int> Infinito(int start)
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
        private IEnumerable<int> InfinitoImpl(int start)
        {
            while (true)
                yield return start++;
        }
    }
}
