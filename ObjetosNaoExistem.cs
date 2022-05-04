namespace NonEcsiste.Objetos;

public abstract class Organismo
{
    public abstract Reino Reino { get; }
    public abstract Ordem Ordem { get; }
    public abstract string Nome();
}

public enum Reino { Animal = 1, Vegetal = 2 }
public enum Ordem { Marsupialia, Chiroptera, Carnivora, Primata }

public abstract class Animal : Organismo
{
    public override Reino Reino { get; } = Reino.Animal;
    public override string Nome() => "Animal genérico";
}

public abstract class Primata : Animal
{
    public override Ordem Ordem { get; } = Ordem.Primata;
    public override string Nome() => "Primata genérico";
}

public class Chimpanze : Primata
{
}

public class Humano : Primata
{
    private readonly string nome;

    public Humano(string nome) => this.nome = nome;

    public override string Nome() => nome;

    public static string NomeGenerico() => "pessoa";
}

class Matematica
{
    int zero = 0;
    public static int Soma(int a, int b) => a + b;
    public int SomaZero(int a) => zero + a;
}

