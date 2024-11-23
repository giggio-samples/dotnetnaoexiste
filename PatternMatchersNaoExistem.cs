namespace NonEcsiste;

public class PatternMatchersNaoExistem
{
    static string Classify(double measurement) => measurement switch
    {
        < -4.0 => "Too low",
        > 10.0 => "Too high",
        double.NaN => "Unknown",
        _ => "Acceptable",
    };


    private static string ClassifyCompiled(double measurement)
    {
        if (!(measurement < -4.0))
        {
            if (!(measurement > 10.0))
            {
                if (double.IsNaN(measurement))
                    return "Unknown";
                return "Acceptable";
            }
            return "Too high";
        }
        return "Too low";
    }

    static string ObterEstacaoDoAno(DateTime data) => data.Month switch
    {
        >= 3 and < 6 => "Outono",
        >= 6 and < 9 => "Inverno",
        >= 9 and < 12 => "Primavera",
        12 or (>= 1 and < 3) => "Verão",
        _ => throw new ArgumentOutOfRangeException(nameof(data), $"Data inesperada: {data.Month}."),
    };

    static string ObterEstacaoDoAnoCompiled(DateTime data)
    {
        switch (data.Month)
        {
            case 3:
            case 4:
            case 5:
                return "Outono";
            case 6:
            case 7:
            case 8:
                return "Inverno";
            case 9:
            case 10:
            case 11:
                return "Primavera";
            case 1:
            case 2:
            case 12:
                return "Verão";
            default:
                throw new ArgumentOutOfRangeException(nameof(data), $"Data inesperada: {data.Month}.");
        }
    }

    static string TakeFive(object input) => input switch
    {
        string { Length: >= 5 } s => s.Substring(0, 5),
        string s => s,
        ICollection<char> { Count: >= 5 } symbols => new string(symbols.Take(5).ToArray()),
        ICollection<char> symbols => new string(symbols.ToArray()),
        null => throw new ArgumentNullException(nameof(input)),
        _ => throw new ArgumentException("Not supported input type."),
    };
}
