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
        const int CHUNK = 4;
        uint[] hash_table = new uint[(int)key.Length];
        
        for (int n = 0; n < key.Length; n++) // Loop over all words in string array
        {
            int letterIdx = 0;
            ulong hashsum = 0;
            int numOfChunks = (int)key[n].Length / CHUNK;
            int arrayLength = numOfChunks;
            int chunkIdx = 0; 

            if(((int)key[n].Length % CHUNK) > 0)
            {
                arrayLength = numOfChunks + 1;
            }
            ulong[] array2sum = new ulong[arrayLength];

            for (int m = 0; m < numOfChunks; m++)
            {
                ulong chunk_val = 0;
                for (int ii = 0; ii < CHUNK; ii++)
                {
                    letterIdx = m * CHUNK + ii;
                    chunk_val = key[n][letterIdx] + PRIME_BASE * chunk_val;
                }
                array2sum[m] = chunk_val;
                chunkIdx += 1;
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

            // Sum all hashed chunks of the string
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
                    if (navne_idx != m) // New name found!
                    {
                        navne_idx = m;      // Update name index
                        unikke_navne += 1;  // Increment counter of unique names
                        matches = 0;        // Reset match counter
                    }

                    matches += 1;

                    fundne_navne[unikke_navne-1, 0] = navne_idx;
                    fundne_navne[unikke_navne-1, 1] = matches;
                }
            }
        }
    }

    public static void writeToFile(uint[] data, string fileName)
    {
        string path = @"C:\Private\Data\FE\Opgave\";
        string fileType = ".csv";

        if ((!File.Exists(path + fileName))) //Checking if 
        {
            FileStream fs = File.Create(path + fileName); //Creates Scores.txt
            fs.Close(); //Closes file stream
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName + fileType))
        {
            for (int i = 0; i < data.Length; i++)
            {
                file.WriteLine(data[i]);
            }
        }

    }

    public static void writeResultToFile(string[] navne, uint[,] searchResult, string fileName, uint unikkeHits)
    {
        string path = @"C:\Private\Data\FE\Opgave\";
        string fileType = ".csv";

        if ((!File.Exists(path + fileName))) //Checking if 
        {
            FileStream fs = File.Create(path + fileName); //Creates Scores.txt
            fs.Close(); //Closes file stream
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName + fileType))
        {
            for (int i = 0; i < unikkeHits; i++)
            {
                file.WriteLine(navne[searchResult[i, 0]].PadRight(20, ' ') + "\t" + searchResult[i, 1]);
                //file.WriteLine(searchResult[i, 1]);
            }
        }
    }

}