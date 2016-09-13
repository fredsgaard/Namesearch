using System.Collections.Generic;
using System;
using System.IO;

public class SearchText
{
    const uint PRIME_BASE = 31;
    const uint PRIME_MOD = 1000000007;

    public static uint[] hash(string[] key)
    {
        uint[] hash_table = new uint[(int)key.Length];

        for (int n = 0; n < key.Length; n++)
        {
            ulong hashval = 0;

            for (int i = 0; i < key[n].Length; i++)
            {
                hashval = key[n][i] + PRIME_BASE * hashval;
            }

            hash_table[n] = (uint)(hashval % PRIME_MOD);
        }

        return hash_table;
    }

    public static uint[] newHash(string[] key)
    {
        const int CHUNK = 4; // Hash 4 bogstaver ad gangen
        uint[] hash_table = new uint[(int)key.Length];
        
        for (int n = 0; n < key.Length; n++) // Loop over all words in string array
        {
            int letterIdx = 0;
            ulong hashsum = 0;
            int numOfChunks = (int)key[n].Length / CHUNK; // Antal chunks i et ord.
            int arrayLength = numOfChunks; // Array-længde til array med hashede chunks
            int chunkIdx = 0; 

            if(((int)key[n].Length % CHUNK) > 0)
            {
                arrayLength = numOfChunks + 1; // Hvis ordlængde ikke er deleligt med CHUNKS array-længde inkremeneteres
            }
            ulong[] array2sum = new ulong[arrayLength]; // Brug ulong[] til midlertidige hash-værdier for at undgå overflow

            for (int m = 0; m < numOfChunks; m++)
            {
                ulong chunk_val = 0; // Midlertidig hash-værdi
                for (int ii = 0; ii < CHUNK; ii++)
                {
                    letterIdx = m * CHUNK + ii;
                    chunk_val = key[n][letterIdx] + PRIME_BASE * chunk_val;
                }
                array2sum[m] = chunk_val; // Skriv hashed chunk til array
                chunkIdx += 1; // Inkrementer hash-index
            }

            // mod(word length, chunk size) ~ 0:
            if(letterIdx < (int)key[n].Length-1)
            {
                ulong chunk_val = 0; 
                for(int j = numOfChunks*CHUNK; j < (int)key[n].Length; j++)
                {
                    chunk_val = key[n][j] + PRIME_BASE * chunk_val; 
                }
                array2sum[chunkIdx] = chunk_val;
            }

            // Summer alle hashede chunks af strengen
            for(int c = 0; c < arrayLength;  c++)
            {
                hashsum += array2sum[c];
            }


            hash_table[n] = (uint)(hashsum % PRIME_MOD);
        }
        return hash_table;
    }

    public static void keywordSearch(uint[] navne, uint[] tekst, ref uint unikke_navne, ref uint[,] fundne_navne)
    {       
        uint navne_idx = 0;
        uint matches = 0;        

        for (uint m = 0; m < navne.Length; m++)
        {
            for (uint n = 0; n < tekst.Length; n++)
            {
                if (navne[m] == tekst[n])
                {
                    if (navne_idx != m) // Nyt navn fundet!
                    {
                        navne_idx = m;      // Update navne index
                        unikke_navne += 1;  // Inkrementer tæller af unikke navne
                        matches = 0;        // Reset match tæller
                    }

                    matches += 1;

                    fundne_navne[unikke_navne-1, 0] = navne_idx;    // Skriv index i hashtabel
                    fundne_navne[unikke_navne-1, 1] = matches;      // Opdater antal match
                }
            }
        }
    }

    public static void writeToFile(uint[] data, string path, string fileName)
    {
        if ((!File.Exists(path + fileName))) //Checking if 
        {
            FileStream fs = File.Create(path + fileName); //Creates Scores.txt
            fs.Close(); //Closes file stream
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName))
        {
            for (int i = 0; i < data.Length; i++)
            {
                file.WriteLine(data[i]);
            }
        }

    }

    public static void writeResultToFile(string[] navne, uint[,] searchResult, string path, string fileName, uint unikkeHits)
    {
        if ((!File.Exists(path + fileName))) //Checking if file exist
        {
            FileStream fs = File.Create(path + fileName); //Creates Scores.txt
            fs.Close(); //Closes file stream
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName))
        {
            for (int i = 0; i < unikkeHits; i++)
            {
                file.WriteLine(navne[searchResult[i, 0]].PadRight(20, ' ') + "\t" + searchResult[i, 1]); // Skriv resultat til fil.
            }
        }
    }

}