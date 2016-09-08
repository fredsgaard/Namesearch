using System.Collections.Generic;

public class SearchText
{
    const uint PRIME_BASE = 257;
    const uint PRIME_MOD = 1000000007;

    public static int[] rabinKarbSearch(string A, string B)
    {
        List<int> retVal = new List<int>();
        ulong siga = 0;
        ulong sigb = 0;
        ulong Q = 100007;
        ulong D = 256;

        for (int i = 0; i < B.Length; ++i)
        {
            siga = (siga * D + (ulong)A[i]) % Q;
            sigb = (sigb * D + (ulong)B[i]) % Q;
        }

        if (siga == sigb)
            retVal.Add(0);

        ulong pow = 1;

        for (int k = 1; k <= B.Length - 1; ++k)
            pow = (pow * D) % Q;

        for (int j = 1; j <= A.Length - B.Length; ++j)
        {
            siga = (siga + Q - pow * (ulong)A[j - 1] % Q) % Q;
            siga = (siga * D + (ulong)A[j + B.Length - 1]) % Q;

            if (siga == sigb)
                if (A.Substring(j, B.Length) == B)
                    retVal.Add(j);
        }

        return retVal.ToArray();
    }

    public static uint hash( string key )
    {
        uint hashval = 0;

        for ( int i = 0; i < key.Length; i++)
        {
            hashval = key[i] + PRIME_BASE * hashval;
        }

        return hashval % PRIME_MOD; 
    }

    public static uint hash_navne(string key)
    {
        uint hashval = 0;

        for (int i = 2; i < key.Length - 2; i++)
        {
            hashval = key[i] + PRIME_BASE * hashval;
        }

        return hashval % PRIME_MOD;
    }
}