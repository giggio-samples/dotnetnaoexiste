using Newtonsoft.Json;

namespace NonEcsiste.TiposNaoExistem;

public class T
{
    static Simples Foo()
    {
        var simples = JsonConvert.DeserializeObject<Simples>("{ a: 1, b: 2 }");
        return simples;
    }
}

public class Simples
{
    public int a { get; set; }
    public int b { get; set; }
}
