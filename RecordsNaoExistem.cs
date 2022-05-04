namespace NonEcsiste.Records;

record Produto(int Id, string Nome);

record struct Cor(string Nome);

readonly record struct CorImutavel(string Nome);
