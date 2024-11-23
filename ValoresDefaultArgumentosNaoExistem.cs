namespace NonEcsiste;

public class ValoresDefaultArgumentosNaoExistem
{
    public void NaoExiste()
    {
        GetInt();
        GetInt(1);
        GetInt(2);
    }
    public void NaoExiste2()
    {
        GetInt(100); // compilado fica assim
        GetInt(1);
        GetInt(2);
    }
    private static void GetInt(int i = 100) { }
}
