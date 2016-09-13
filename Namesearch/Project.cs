using System;
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

            SearchText.writeToFile(tekst_hash, "tekst_hash");
            SearchText.writeToFile(navne_hash, "navne_hash");

            uint[,] fundne_navne = new uint[navne.Length, 2];
            uint unikke_hits = 0;

            // Find alle forekomster af hashede navne i den hashede tekst og returner i array fundne_navne og unikke hits.
            SearchText.keywordSearch(navne_hash, tekst_hash, ref unikke_hits, ref fundne_navne);
            SearchText.writeResultToFile(navne, fundne_navne, "searchResult", unikke_hits);
              
            // Print resultater til konsollen
            for (uint n = 0; n < unikke_hits; n++)
                Console.WriteLine("Navn : {0}Antal forekomster: {1}", navne[fundne_navne[n, 0]].PadRight(15, ' '), fundne_navne[n, 1]);

            Console.ReadKey();

            return 0;
        }
    }
}