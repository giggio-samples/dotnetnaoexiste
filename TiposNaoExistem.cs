using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonEcsiste
{
    public class TiposNaoExistem
    {
        Simples Foo()
        {
            System.Object o;
            var simples = JsonConvert.DeserializeObject<Simples>("{ a: 1, b: 2 }");
            return simples;
        }
    }

    public class Simples
    {
        public int a { get; set; }
        public int b { get; set; }
    }
}
