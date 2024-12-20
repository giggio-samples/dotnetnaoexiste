﻿namespace NonEcsiste;

public class OperadoresNaoExistem
{
    private static readonly Person pessoa = new Person(null);

    static void M()
    {
        var nome = pessoa?.Nome;
        // vira:
        if (pessoa != null)
			nome = pessoa.Nome;
    }

    static void N()
    {
        var outraPessoa = pessoa ?? new Person(null);
        // vira:
        if (pessoa == null)
            outraPessoa = new Person(null);
    }

    static void O()
    {
        Person maisOutraPessoa;
        if (pessoa is null)
            maisOutraPessoa = pessoa;
        else
            maisOutraPessoa = new Person(null);
    }

    record Person(string Nome);
}
