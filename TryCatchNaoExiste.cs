using System;
using static System.Console;

namespace NonEcsiste
{
    public class TryCatchNaoExiste
    {
        public void TC()
        {
            WriteLine("1");
            try
            {
                WriteLine("2");
            }
            catch (NullReferenceException nre)
            {
                WriteLine("3");
                if (DateTime.Now.Second > 30)
                    throw;
                else
                    throw new Exception("erro", nre);
            }
            finally
            {
                WriteLine("4");
            }
            WriteLine("5");
        }
    }
}
