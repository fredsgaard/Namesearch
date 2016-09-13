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
            
            Console.WriteLine("Angiv sti til navne-fil:");
            string path = Console.ReadLine();

            Console.WriteLine("Angiv navn på txt navne-fil:");
            string fileName_navne = Console.ReadLine();
            string navne_liste = File.ReadAllText(path + fileName_navne);
            string[] navne = navne_liste.Split(delimiterChars);
            uint[] navne_hash = new uint[(int)navne.Length];

            Console.WriteLine("Angiv navn på txt tekst-fil:");
            string fileName_tekst = Console.ReadLine();
            string tekst = File.ReadAllText(path + fileName_tekst);            
            string[] ord = tekst.Split(delimiterChars);
            uint[] tekst_hash = new uint[(int)ord.Length];

            // Hash navne og ord
            tekst_hash = SearchText.newHash(ord);
            navne_hash = SearchText.newHash(navne);
           
            SearchText.writeToFile(tekst_hash, path, "tekst_hash.csv");
            SearchText.writeToFile(navne_hash, path,  "navne_hash.csv");

            uint[,] fundne_navne = new uint[navne.Length, 2];
            uint unikke_hits = 0;

            // Find alle forekomster af hashede navne i den hashede tekst og returner i array fundne_navne og unikke hits.
            SearchText.keywordSearch(navne_hash, tekst_hash, ref unikke_hits, ref fundne_navne);

            // Skriv resultat til fil
            SearchText.writeResultToFile(navne, fundne_navne, path, "searchResult.csv", unikke_hits);
              
            // Print resultater til konsollen
            for (uint n = 0; n < unikke_hits; n++)
                Console.WriteLine("Navn : {0}Antal forekomster: {1}", navne[fundne_navne[n, 0]].PadRight(15, ' '), fundne_navne[n, 1]);

            Console.ReadKey();

            return 0;
        }
    }
}