﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Namesearch
{
    class Project
    {
        static int Main(string[] args)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', ';', '\''};

            string path_navne = @"C:\Private\Data\FE\Opgave\danske_drengenavne.txt";
            string navne_liste = File.ReadAllText(path_navne);
            string[] navne = navne_liste.Split(delimiterChars);
            uint[] navne_hash = new uint[(int)navne.Length];

            string path_tekst = @"C:\Private\Data\FE\Opgave\sample_tekst.txt";
            string tekst = File.ReadAllText(path_tekst);
            //string tekst = "Denne tekst indeholder to pæne navne: Morten og Peter. Peter har en speciallægepraksis";
            string[] ord = tekst.Split(delimiterChars);
            uint[] tekst_hash = new uint[(int)ord.Length];

            // Hash navne og ord
            tekst_hash = SearchText.newHash(ord);
            navne_hash = SearchText.newHash(navne);                                 

            uint[,] fundne_navne = new uint[navne.Length, 2];
            uint unikke_hits = 0;

            // Find alle forekomster af hashede navne i den hashede tekst og returner i array fundne_navne og unikke hits.
            SearchText.keywordSearch(navne_hash, tekst_hash, ref unikke_hits, ref fundne_navne);
            
            // Print resultater til konsollen
            for (uint n = 0; n < unikke_hits; n++)
                Console.WriteLine("Navn : {0},\tHash index : {1}, \tAntal forekomster: {2}", navne[fundne_navne[n, 0]], fundne_navne[n, 0], fundne_navne[n, 1]);

            Console.ReadKey();

            return 0;
        }
    }
}
// if ((!File.Exists(@"C:\Private\Data\FE\Opgave\tekst_hash.txt"))) //Checking if scores.txt exists or not
//            {
//                FileStream fs = File.Create(@"C:\Private\Data\FE\Opgave\tekst_hash.txt"); //Creates Scores.txt
//fs.Close(); //Closes file stream
//            }

//            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Private\Data\FE\Opgave\tekst_hash.txt"))
//            {
//                for (int i = 0; i<tekst_hash.Length; i++)
//                {
//                    file.WriteLine(tekst_hash[i]);
//                }
//            }

//            if ((!File.Exists(@"C:\Private\Data\FE\Opgave\navne_hash.txt"))) //Checking if scores.txt exists or not
//            {
//                FileStream fs = File.Create(@"C:\Private\Data\FE\Opgave\navne_hash.txt"); //Creates Scores.txt
//fs.Close(); //Closes file stream
//            }

//            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Private\Data\FE\Opgave\navne_hash.txt"))
//            {
//                for (int i = 0; i<navne_hash.Length; i++)
//                {
//                    file.WriteLine((UInt32)navne_hash[i]);                    
//                }
////            }
//if ((!File.Exists(@"C:\Private\Data\FE\Opgave\search_result.txt"))) //Checking if scores.txt exists or not
//            {
//                FileStream fs = File.Create(@"C:\Private\Data\FE\Opgave\search_result.txt"); //Creates Scores.txt
//fs.Close(); //Closes file stream
//            }

//            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Private\Data\FE\Opgave\search_result.txt"))
//            {
//                for (int i = 0; i<navne.Length; i++)
//                {
//                    file.WriteLine(fundne_navne[i, 0]);
//                    file.WriteLine(fundne_navne[i, 1]);
//                }
//            }

//            var result = File.ReadAllLines(@"C:\Private\Data\FE\Opgave\search_result.txt");
//uint[,] result_parsed = new uint[(int)result.Length / 2, 2];
//int resultIdx = 0;
//            while (resultIdx<result.Length / 2 - 1)
//            {
//                result_parsed[resultIdx, 0] = UInt32.Parse(result[resultIdx * 2]);
//                result_parsed[resultIdx, 1] = UInt32.Parse(result[resultIdx * 2 + 1]);
//                resultIdx++;
//            }