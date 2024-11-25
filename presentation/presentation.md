---
title: Desconstruindo o .NET
author: Giovanni Bassi
theme:
  name: dark
options:
  command_prefix: "cmd:"
---

Giovanni Bassi
---

<!-- cmd:column_layout: [1, 1] -->

<!-- cmd:column: 0 -->

Em sabático

Microsoft MVP há 15 anos

Programador

Escalador, ciclista, motociclista, mecânico amador

<!-- cmd:column: 1 -->
![](bike.jpg)
![](escalada.jpg)
![](moto.jpg)
![](railsconf.jpg)

https://bsky.app/profile/giggio.net

https://twitter.com/giovannibassi

https://linkd.in/gbassi

<!-- cmd:end_slide -->

CLR
---
<!-- cmd:column_layout: [1, 3, 1] -->
<!-- cmd:column: 1 -->
“A principal meta da CLR é tornar a programação fácil”

<!-- cmd:end_slide -->

Açúcar sintático (syntax sugar)
---

* Várias funcionalidades/sintaxes do C# não existem na IL ou no código final compilado
* Muitas destas dependem de tipos específicos (tuplas, por exemplo)

<!-- cmd:end_slide -->

Tipos não existem
---

* A profunda ligação da linguagem com o sistema de tipos
* Quem é System.Object, onde ele está, que API suporta?
* Como o .NET Standard funciona
* Forward lookup types

<!-- cmd:end_slide -->

Exemplo .NET Standard: JSON.NET
---

```csharp
JsonConvert.DeserializeObject<Simples>("{ }");
```

```mermaid +render
flowchart LR
    minhaapp[Minha app]
    jsonnet[JSON.NET]
    netstd[.NET Standard]
    net9[.NET Standard]
    minhaapp --> jsonnet
    jsonnet --> netstd
    netstd --> net9
    minhaapp --> net9
```

<!-- cmd:end_slide -->

Using não existe
---

* A dependência de IDisposable e IAsyncDisposable
* Como o código que usa `using` e `await using` é compilado

<!-- cmd:end_slide -->

Using
---

```csharp
var buffer = new byte[] { 1, 2, 3 };
using (var ms = new MemoryStream())
{
    ms.Write(buffer, 0, 3);
}

// ou

var buffer = new byte[] { 1, 2, 3 };
using var ms = new MemoryStream();
ms.Write(buffer, 0, 3);
```

<!-- cmd:end_slide -->

Using: IL
---

```asm
// byte[] buffer = new byte[3] { 1, 2, 3 };
// using MemoryStream memoryStream = new MemoryStream();
IL_0013: newobj instance void [System.Runtime]System.IO.MemoryStream::.ctor()
IL_0018: stloc.1
.try
{
    // memoryStream.Write(buffer, 0, 3);
    // oculto…
    IL_001e: callvirt instance void [System.Runtime]System.IO.Stream::Write(uint8[], int32, int32)
    // (no C# code)
    IL_0025: leave.s IL_0032
}
finally
{
    IL_0027: ldloc.1
    IL_0028: brfalse.s IL_0031
    IL_002a: ldloc.1
    IL_002b: callvirt instance void [System.Runtime]System.IDisposable::Dispose()
    IL_0031: endfinally
}
```
<!-- cmd:end_slide -->

Tuplas existem, mas...​
---

As tuplas existem, mas a linguagem facilita​

Os nomes dos valores das tuplas não existem​

<!-- cmd:end_slide -->

Tuplas: esses código fazem a mesma coisa​
---

<!-- cmd:column_layout: [1, 1] -->

<!-- cmd:column: 0 -->
```csharp
(int a, int b) Retorna()​
{​
    var t = (a: 1, b: 2);​
    return t;​
}​

public int Obtem1()​
{​
    var (i, j) = Retorna();​
    return i + j;​
} ​
```

<!-- cmd:column: 1 -->
```csharp
static (int c, int d) Retorna2()​
{​
    var t = (c: 1, d: 2);​
    return t;​
}​
 ​
public int Obtem2()​
{​
    var t = Retorna2();​
    var k = t.Item1;​
    var l = t.Item2;​
    return k + l;​
}​
```

<!-- cmd:end_slide -->

Tuplas: o que acontece​
---

```csharp
[return: TupleElementNames(new string[] { "a", "b" })]​
private ValueTuple<int, int> Retorna()​
{​
    return new ValueTuple<int, int>(1, 2);​
}​

public int Obtem1()​
{​
    ValueTuple<int, int> valueTuple = Retorna();​
    int i = valueTuple.Item1;​
    int j = valueTuple.Item2;​
    return i + j;​
}​
```

<!-- cmd:end_slide -->

Valores default para argumentos não existem​
---

Quer dizer, existe mais ou menos...​

No fundo, é quase tudo mentira do compilador​

Código que você escreve:

```csharp
public void NaoExiste()​
{​
    GetInt();​
    GetInt(1);​
    GetInt(2);​
}​
private static void GetInt(int i = 100) { }​
```

<!-- cmd:pause -->

Compilado:

```csharp
public void NaoExiste()​
{​
    GetInt(100); // compilado fica assim​
    GetInt(1);​
    GetInt(2);​
}​
private static void GetInt(int i = 100) { }​
```

<!-- cmd:end_slide -->

Try/catch existe​
---

Mas não é exatamente o que você imagina

```csharp
try​
{​
    WriteLine("2");​
}​
catch (NullReferenceException nre)​
{​
    WriteLine("3");​
    if (DateTime.Now.Second > 30)​
        throw;​
    else​
        throw new Exception("erro", nre);​
}​
finally​
{​
    WriteLine("4");​
}​
```

<!-- cmd:end_slide -->

try/catch/finally (IL)​
---

```asm
.try​
{​
    .try​
    {​
        // Console.WriteLine("2");​
        IL_0014: leave.s IL_004b​
    }​
    catch [System.Runtime]System.NullReferenceException​
    {​
        // catch (NullReferenceException innerException)​
        // Console.WriteLine("3");​
        // if (DateTime.Now.Second > 30)​
        // throw;​
        IL_0032: rethrow​
        // throw new Exception("erro", innerException);​
        IL_0034: ldstr "erro"​
        IL_0039: ldloc.0​
        IL_003a: newobj instance void [System.Runtime]System.Exception::.ctor(string, class [System.Runtime]System.Exception)​
        IL_003f: throw​
    }​
} // end .try​
finally​
{​
    // Console.WriteLine("4");​
    IL_004a: endfinally​
}​
IL_004b: ...​
```

<!-- cmd:end_slide -->

Goto existe​
---

E é quase o que você imagina​

Otimizações na IL para casos específicos​

<!-- cmd:end_slide -->

LINQ não existe​
---

A dependência de IQueryable e IEnumerable​
​
Como o código que usa LINQ é compilado​

Como o "LINQ to" funciona​

<!-- cmd:end_slide -->

LINQ to objects
---

```csharp
var ns = new[] { 1, 2, 3 };​
var maioresQue1 = from n in ns​
                  where n > 1​
                  select n;​

// como se fosse:​
// var maioresQue1 = Enumerable.Where(ns, n => n > 1);​
foreach (var n in maioresQue1)​
    WriteLine(n);​
```

<!-- cmd:end_slide -->

LINQ to Queryable​
---

```csharp
var pessoas = ObterPessoas();​
var famigliaBassi = from n in pessoas​
                    where n.SobreNome == "Bassi"​
                    select n;​

// quase como se fosse:​
ParameterExpression pessoaParameter = Expression.Parameter(typeof(Pessoa), "n");​
var famigliaBassi = Queryable.Where(pessoas,​
    Expression.Lambda<Func<Pessoa, bool>>(​
        Expression.Equal(Expression.Property(pessoaParameter, Pessoa.SobreNome),​
        Expression.Constant("Bassi", typeof(string))), [pessoaParameter])​
); ​
```

<!-- cmd:end_slide -->

Yield não existe​
---

A dependência de IEnumerable e IAsyncEnumerable​

Falando um pouco de IL​

Como um método com iterator é compilado​

Algumas dicas de desempenho​

O que você escreve:

```csharp
static IEnumerable<int> Enum()​
{​
    WriteLine("um");​
    yield return 1;​
    WriteLine("dois");​
    yield return 2;​
    WriteLine("tres");​
    yield return 3;​
}​
```

O que é compilado:

```csharp
[IteratorStateMachine(​typeof(<Enum>d__0))]​
private static IEnumerable<int> Enum()​
{​
    return new <Enum>d__0(-2);​
}​
```

<!-- cmd:end_slide -->

Yield: o que é compilado
---

```csharp
private bool MoveNext()​
{​
    switch (<>1__state)​
    {​
        default:​
            return false;​
        case 0:​
            <>1__state = -1;​
            Console.WriteLine("um");​
            <>2__current = 1;​
            <>1__state = 1;​
            return true;​
        case 1:​
            <>1__state = -1;​
            Console.WriteLine("dois");​
            <>2__current = 2;​
            <>1__state = 2;​
        return true;​
            case 2:​
            <>1__state = -1;​
            Console.WriteLine("tres");​
            <>2__current = 3;​
            <>1__state = 3;​
            return true;​
        case 3:​
            <>1__state = -1;​
            return false;​
    }​
}​
```

<!-- cmd:end_slide -->

Async/await não existem​
---

A dependência de “awaitables” (e Task, ValueTask)​

Falando mais um pouco de IL​

Como um método assíncrono é compilado​

Algumas dicas de desempenho​

O que você escreve:

```csharp
async Task FooAsync()​
{​
    int n = GetInt();​
    WriteLine(n);​
    await Task.Delay(100);​
    int o = GetInt(n);​
    await Task.Delay(200);​
    int p = GetInt(o);​
    WriteLine(p);​
    var text = await File.ReadAllTextAsync(file);​
    WriteLine(text);​
}​
```

O que é compilado:

```csharp
[AsyncStateMachine(typeof(<FooAsync>d__1))]​
private Task FooAsync()​
{​
    <FooAsync>d__1 stateMachine = default(<FooAsync>d__1);​
    stateMachine.<>t__builder = AsyncTaskMethodBuilder.Create();​
    stateMachine.<>4__this = this;​
    stateMachine.<>1__state = -1;​
    stateMachine.<>t__builder.Start(ref stateMachine);​
    return stateMachine.<>t__builder.Task;​
}​
```

<!-- cmd:end_slide -->

O que é compilado
---

<!-- cmd:column_layout: [1, 1, 1] -->

<!-- cmd:column: 0 -->

```csharp +line_numbers {12-14}
private void MoveNext()​
{​
    int num = <>1__state;​
    AsyncNaoExiste asyncNaoExiste = <>4__this;​
    try​
    {​
        TaskAwaiter awaiter2;​
        TaskAwaiter<string> awaiter;​
        switch (num)​
        {​
        default:​
            <n>5__2 = GetInt(0);​
            Console.WriteLine(<n>5__2);​
            awaiter2 = Task.Delay(100).GetAwaiter();​
            if (!awaiter2.IsCompleted)​
            {​
                num = (<>1__state = 0);​
                <>u__1 = awaiter2;​
                <>t__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);​
                return;​
            }​
            goto IL_008c;​
        case 0:​
            awaiter2 = <>u__1;​
            <>u__1 = default(TaskAwaiter);​
            num = (<>1__state = -1);​
            goto IL_008c;​
```

<!-- cmd:column: 1 -->

```csharp +line_numbers {15-16}
        case 1:​
            awaiter2 = <>u__1;​
            <>u__1 = default(TaskAwaiter);​
            num = (<>1__state = -1);​
            goto IL_00fc;​
        case 2:​
            {​
                awaiter = <>u__2;​
                <>u__2 = default(TaskAwaiter<string>);​
                num = (<>1__state = -1);​
                break;​
            }​
            IL_00fc:​
            awaiter2.GetResult();​
            Console.WriteLine(GetInt(<o>5__3));​
            awaiter = File.ReadAllTextAsync(asyncNaoExiste.file).GetAwaiter();​
            if (!awaiter.IsCompleted)​
            {​
                num = (<>1__state = 2);​
                <>u__2 = awaiter;​
                <>t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);​
                return;​
            }​
            break;​
```

<!-- cmd:column: 2 -->

```csharp +line_numbers {3,4,14}
            IL_008c:​
            awaiter2.GetResult();​
            <o>5__3 = GetInt(<n>5__2);​
            awaiter2 = Task.Delay(200).GetAwaiter();​
            if (!awaiter2.IsCompleted)​
            {​
                num = (<>1__state = 1);​
                <>u__1 = awaiter2;​
                <>t__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);​
                return;​
            }​
            goto IL_00fc;​
        }​
        Console.WriteLine(awaiter.GetResult());​
    }​
    catch (Exception exception)​
    {​
        <>1__state = -2;​
        <>t__builder.SetException(exception);​
        return;​
    }​
    <>1__state = -2;​
    <>t__builder.SetResult();​
}​
```

<!-- cmd:end_slide -->

Viram aqueles inteiros locais na heap?​
---

Cuidado com a afirmação de que toda struct vai para heap e toda class para stack​

Acabamos de ver um exemplo de variáveis locais na heap com o código async​

```csharp
<n>5__2 = GetInt(0);​
Console.WriteLine(<n>5__2);​
```

<!-- cmd:end_slide -->

Nullable reference types não existem​
---
A dependência de Nullable reference types e ​ NullableContextAttribute e NullableAttribute​

IL fica praticamente inalterada​

```csharp
public static string? ProduceNullableString() => ...;​
```

Ganha o atributo `[NullableContext(2)]​`:

```csharp
[NullableContext(2)]​
public static string ProduceNullableString() => ...;​
```

E...

```csharp
static void TakesStringAndNullableString(string text,​ string? nullableString) =>​ WriteLine(text);​
```

Ganha o atributo `[NullableContext(1)]​` e `[Nullable(2)]` no método:

```csharp
[NullableContext(1)]​
static void TakesStringAndNullableString(string text,​ [Nullable(2)] string nullableString) =>​ WriteLine(text);​
```

<!-- cmd:end_slide -->

Discards não existem​
---

Para retornos são ignorados​

Para passagem de valores é atribuído para uma variável que é criada e não é usada depois​

<!-- cmd:end_slide -->

Alguns operadores não existem​
---

`?.​`

`??`

`is` vira `==​`

```csharp
var nome = pessoa?.Nome;​
```

Vira:​

```csharp
if (pessoa != null)​
    nome = pessoa.Nome;​

```

E...

```csharp
var outraPessoa = pessoa ?? new Person(null);​
```

Vira:​

```csharp
if (pessoa == null)​
    outraPessoa = new Person(null);​
```

<!-- cmd:end_slide -->
Mais alguns rápidos
---

Top Level statements não existem

* Você sempre tem uma classe e um método Main​

Tipos que não existem​

* Records são somente classes e structs com uns membros a mais​
* readonly structs são só structs com um atributo​

Init (propriedades) não existe​

* É só um set​

<!-- cmd:end_slide -->

Pattern matchers não existem​
---

São só `==`, `is`, `switch` e `:?`​

```csharp
static string Classify(double m)​ =>​
m switch​
{​
    < -4.0 => "Too low",​
    > 10.0 => "Too high",​
    double.NaN => "Unknown",​
    _ => "Acceptable",​
};​
```

Vira:

```csharp
private static string Classify(double m)​
{​
    if (!(m < -4.0))​
    {​
        if (!(m > 10.0))​
        {​
            if (double.IsNaN(m))​
                return "Unknown";​
            return "Acceptable";​
        }​
        return "Too high";​
    }​
    return "Too low";​
}​
```

<!-- cmd:end_slide -->

Pattern matchers não existem​: switch com ranges
---
<!-- cmd:column_layout: [1, 1] -->

<!-- cmd:column: 0 -->

```csharp
static string ObterEstacao(​DateTime data) =>
data.Month switch​
{​
    >= 3 and < 6 => "Outono",​
    >= 6 and < 9 => "Inverno",​
    >= 9 and < 12 => "Primavera",​
    12 or (>= 1 and < 3) => "Verão",​
    _ => throw new Exception(),​
};​
```

<!-- cmd:column: 1 -->

```csharp
static string ObterEstacao(DateTime data)​
{​
    switch (data.Month)​
    {​
        case 3:​
        case 4:​
        case 5:​
            return "Outono";​
        case 6:​
        case 7:​
        case 8:​
            return "Inverno";​
        case 9:​
        case 10:​
        case 11:​
            return "Primavera";​
        case 1:​
        case 2:​
        case 12:​
            return "Verão";​
        default:​
            throw new Exception(),​
    }​
}​
```

<!-- cmd:end_slide -->

Pattern matchers não existem​: match no tipo
---

```csharp
static string TakeFive(object input) => input switch​
{​
    string { Length: >= 5 } s => s.Substring(0, 5),​
​
    string s => s,​
​
    ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),​
​
    ICollection<char> symbols => new string(symbols.ToArray()),​
​
    null => throw new ArgumentNullException(nameof(input)),​

    _ => throw new ArgumentException("Not supported input type."),​
};​
```

Isso tudo vira `if`.

<!-- cmd:end_slide -->

Orientação a objetos não existe​
---

Estrutura de um objeto (estrutura de dados)​

Como funciona um método​

* Diferença entre método estático e método de instância​
* Como funcionam overrides/Vtable​
* Inlining​

<!-- cmd:end_slide -->

Referências​
---

Book of the runtime:​
https://bit.ly/clr-botr​

Repositório com exemplos:​
https://github.com/giggio-samples/dotnetnaoexiste​

<!-- cmd:end_slide -->

---

Obrigado
---

![](tks.jpg)