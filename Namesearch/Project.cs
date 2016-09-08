using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Namesearch
{
    class Project
    {
        static int Main(string[] args)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', ';'};

            string path_navne = @"C:\Private\Data\FE\Opgave\danske_drengenavne.txt";
            //string path_navne = @"C:\Private\Data\FE\Opgave\drengenavne_test.txt";
            string navne_liste = File.ReadAllText(path_navne);
            string[] navne = navne_liste.Split(delimiterChars);
            uint[] navne_hash = new uint[(int)navne.Length];

            string path_tekst = @"C:\Private\Data\FE\Opgave\sample_tekst.txt";
            //string path_tekst = @"C:\Private\Data\FE\Opgave\sample_tekst_test.txt";
            string tekst = File.ReadAllText(path_tekst);
            string[] ord = tekst.Split(delimiterChars);
            uint[] tekst_hash = new uint[(int)ord.Length];

            // Hash navne og ord
            for (int n = 0; n < ord.Length; n++)
            {
                tekst_hash[n] = SearchText.hash(ord[n]);
            }

            for (int n = 0; n < navne.Length; n++)
            {
                navne_hash[n] = SearchText.hash( navne[n] );
            }

            uint[,] fundne_navne = new uint[(int)navne.Length, 2];
            uint navne_idx = 0;
            uint matches = 0;
            uint unikke_navne = 0;

            for (uint m = 0; m < navne_hash.Length; m++)
            {
                for (uint n = 0; n < tekst_hash.Length; n++)
                {
                    if (navne_hash[m] == tekst_hash[n])
                    {
                        matches += 1;
                        fundne_navne[unikke_navne, 0] = navne_idx;
                        fundne_navne[unikke_navne, 1] = matches;
                        
                        if (navne_idx != m) // New name found!
                        {
                            navne_idx = m;      // Update name index
                            unikke_navne += 1;  // Increment counter of unique names
                            matches = 0;        // Reset match counter
                        }
                    }
                }
            }
            
            Console.WriteLine("Unikke navne: {0}", unikke_navne);

            for (uint n = 0; n < unikke_navne; n++)
                Console.WriteLine("Navn : {0},\tAntal forekomster: {1}", navne[fundne_navne[n, 0]], fundne_navne[n, 1]);

            Console.ReadKey();

            return 0;
        }
    }
}
