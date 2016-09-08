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

            //string path_navne = @"C:\Private\Data\FE\Opgave\danske_drengenavne.txt";
            string path_navne = @"C:\Private\Data\FE\Opgave\drengenavne_test.txt";
            string navne_liste = File.ReadAllText(path_navne);
            string[] navne = navne_liste.Split(delimiterChars);
            uint[] navne_hash = new uint[(int)navne.Length];

            //string path_tekst = @"C:\Private\Data\FE\Opgave\sample_tekst.txt";
            string path_tekst = @"C:\Private\Data\FE\Opgave\sample_tekst_test.txt";
            string tekst = File.ReadAllText(path_tekst);
            string[] ord = tekst.Split(delimiterChars);
            uint[] tekst_hash = new uint[(int)ord.Length];

            foreach (string s in navne)
                Console.WriteLine("Navn: {0}", s);

            foreach (string s in ord)
                Console.WriteLine("Ord: {0}", s);


            // Hash navne og ord
            for (int n = 0; n < ord.Length; n++)
            {
                tekst_hash[n] = SearchText.hash(ord[n]);
            }

            for (int n = 0; n < navne.Length; n++)
            {
                navne_hash[n] = SearchText.hash( navne[n] );
            }

            //for (int i = 0; i < 6; i++)
            //    Console.WriteLine("Navn: {0}", navne_hash[i]);

            //Array.Sort(navne_hash);
            foreach (uint n in navne_hash)
                Console.WriteLine("Sorted hash, navne: {0}", n);

            //Array.Sort(navne_hash);
            foreach (uint n in tekst_hash)
                Console.WriteLine("Sorted hash, tekst: {0}", n);

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
                        //if (navne_idx != m) // New name found!
                        //{
                        //    navne_idx = m;
                        //    unikke_navne += 1;
                        //}
                        //fundne_navne[unikke_navne, 1] = m;
                        //fundne_navne[unikke_navne, 2] = matches;                        
                    }
                }
            }

            Console.WriteLine("Matches: {0}", matches);

            //for (uint n = 0; n < unikke_navne; n++)
            //    Console.WriteLine("Navn (Hash): {0}, Antal forekomster: {1}", fundne_navne[n,1], fundne_navne[n,2]);

            Console.ReadKey();

            return 0;
        }
    }
}
