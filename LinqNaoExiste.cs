using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace NonEcsiste.LinqNaoExiste;

public class LinqNaoExiste
{
    public static void LinqToObjects()
    {
        var ns = new[] { 1, 2, 3 };
        var maioresQue1 = from n in ns
                          where n > 1
                          select n;
        // como se fosse:
        // var maioresQue1 = Enumerable.Where(ns, n => n > 1);
        foreach (var n in maioresQue1)
            WriteLine(n);
    }
    public static void LinqToGiggio()
    {
        var pessoas = ObterPessoas();
        var famigliaBassi = from n in pessoas
                            where n.SobreNome == "Bassi"
                            select n;
        // quase como se fosse:
        // ParameterExpression pessoaParameter = Expression.Parameter(typeof(Pessoa), "n");
        // var famigliaBassi  = Queryable.Where(pessoas,
        //     Expression.Lambda<Func<Pessoa, bool>>(
        //         Expression.Equal(Expression.Property(pessoaParameter, Pessoa.SobreNome),
        //         Expression.Constant("Bassi", typeof(string))), [pessoaParameter]));
        foreach (var pessoa in famigliaBassi)
            WriteLine(pessoa);
    }

    static IQueryable<Pessoa> ObterPessoas()
    {
        var pessoas = new[]
        {
            new Pessoa { Nome = "Giovanni", SobreNome = "Bassi" },
            new Pessoa { Nome = "João", SobreNome = "Silva" }
        };
        return new PessoaQuery<Pessoa>(new PessoaQueryProvider(pessoas));
    }
}

public class Pessoa
{
    public string Nome { get; set; }
    public string SobreNome { get; set; }
    public override string ToString() => $"{Nome} {SobreNome}";
}

#region Linq provider
public class PessoaQueryProvider : IQueryProvider
{
    private readonly IEnumerable<Pessoa> pessoas;

    public PessoaQueryProvider(IEnumerable<Pessoa> pessoas) => this.pessoas = pessoas ?? throw new ArgumentNullException(nameof(pessoas));

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new PessoaQuery<TElement>(this, expression);

    public IQueryable CreateQuery(Expression expression) => new PessoaQuery<Pessoa>(this, expression);

    public TElement Execute<TElement>(Expression expression) => (TElement)Execute(expression);

    public static string GetQueryText(Expression expression) => "pessoas"; // todo

    public object Execute(Expression expression) => new PessoaQueryTranslator().Translate(pessoas, expression);
}

public class PessoaQuery<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
{
    PessoaQueryProvider provider;
    Expression expression;

    public PessoaQuery(PessoaQueryProvider provider)
    {
        this.provider = provider ?? throw new ArgumentNullException("provider");
        expression = Expression.Constant(this);
    }

    public PessoaQuery(PessoaQueryProvider provider, Expression expression)
    {
        this.provider = provider ?? throw new ArgumentNullException("provider");
        this.expression = expression ?? throw new ArgumentNullException("expression");
        if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            throw new ArgumentOutOfRangeException("expression");
    }

    Expression IQueryable.Expression => expression;

    Type IQueryable.ElementType => typeof(T);

    IQueryProvider IQueryable.Provider => provider;

    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)provider.Execute(expression)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)provider.Execute(expression)).GetEnumerator();
}

public class PessoaQueryTranslator : ExpressionVisitor
{
    private IEnumerable<Pessoa> pessoas;

    public IEnumerable<Pessoa> Translate(IEnumerable<Pessoa> pessoas, Expression expression)
    {
        this.pessoas = pessoas;
        Visit(expression);
        return this.pessoas;
    }

    protected override Expression VisitMethodCall(MethodCallExpression m)
    {
        if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
        {
            var lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
            Visit(lambda.Body);
            return m;
        }
        throw new NotSupportedException($"The method '{m.Method.Name}' is not supported");
    }

    protected override Expression VisitBinary(BinaryExpression b)
    {
        switch (b.NodeType)
        {
            case ExpressionType.Equal:
                if (b.Left.NodeType == ExpressionType.MemberAccess && b.Left is MemberExpression memberExpression)
                {
                    if (b.Right.NodeType == ExpressionType.Constant)
                    {
                        var property = memberExpression.Member.Name;
                        if (property == "SobreNome")
                        {
                            var sobreNome = (string)((ConstantExpression)b.Right).Value;
                            var list = new List<Pessoa>();
                            foreach (var p in pessoas)
                                if (p.SobreNome == sobreNome)
                                    list.Add(p);
                            pessoas = list;
                        }
                        else
                        {
                            throw new NotSupportedException($"Can't resolve operations on property '{property}'");
                        }
                    }
                    else
                    {
                        throw new NotSupportedException($"Can't resolve operations on right type '{b.Right.NodeType}'");
                    }
                }
                else
                {
                    throw new NotSupportedException($"Can't resolve operations on left node type '{b.Left.NodeType}'");
                }
                break;
            default:
                throw new NotSupportedException($"The binary operator '{b.NodeType}' is not supported");
        }
        return b;
    }

    private static Expression StripQuotes(Expression e)
    {
        while (e.NodeType == ExpressionType.Quote)
            e = ((UnaryExpression)e).Operand;
        return e;
    }
}
#endregion
