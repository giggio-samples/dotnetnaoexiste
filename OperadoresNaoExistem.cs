namespace NonEcsiste.OperadoresNaoExistem
{
    public class OperadoresNaoExistem
    {
        private static readonly Person pessoa = new Person(null);

        void M()
        {
            var nome = pessoa?.Nome;
        }

        void N()
        {
            var outraPessoa = pessoa ?? new Person(null);
        }
        void O()
        {
            Person maisOutraPessoa;
            if (pessoa is null)
                maisOutraPessoa = pessoa;
            else
                maisOutraPessoa = new Person(null);
        }

        record Person(string Nome);
    }
}
