using System;
using static System.Console;

namespace NonEcsiste
{
    public class GotoExiste
    {
        public void GE()
        {
            WriteLine("antes do if");
            if (DateTime.Now.Second > 30)
                goto meuLabel;
            WriteLine("depois do if");
            goto meuOutroLabel;
        meuLabel:
            WriteLine("depois do primeiro label");
        meuOutroLabel:
            WriteLine("depois do outro label");
        }

    }
}
