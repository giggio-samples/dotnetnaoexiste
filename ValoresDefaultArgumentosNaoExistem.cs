﻿namespace NonEcsiste;

public class ValoresDefaultArgumentosNaoExistem
{
    public void NaoExiste()
    {
        _ = GetInt();
        _ = GetInt(1);
        _ = GetInt(2);
    }
    private static int GetInt(int i = 100) => DateTime.Now.Second + i;
}
