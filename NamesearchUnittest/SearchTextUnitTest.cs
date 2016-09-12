using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NamesearchUnittest
{
    [TestClass]
    public class SearchTextUnitTest
    {
        [TestMethod]
        public void hashUnittest()
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t', ';', '\'' };

            // Arrange
            string text = "Denne tekst indeholder to pæne navne: Morten og Peter. Peter har en speciallægepraksis";            
            string[] words = text.Split(delimiterChars); 
            uint[] hashed_words_expected = new uint[15] { 2126470, 3556365, 6448594, 3707, 3561133, 3374096, 0, 2407469, 3544, 2484152, 0, 2484152, 103065, 3241, 17211904 };
            uint[] hashed_words_actual;

            // Act
            //hashed_words_actual = SearchText.hash(words);
            hashed_words_actual = SearchText.newHash(words);
            
            // Assert            
            CollectionAssert.AreEqual(hashed_words_expected, hashed_words_actual);
        }

        [TestMethod]
        public void keywordSearchUnittest()
        {
            // Arrange
            var navne = File.ReadAllLines(@"C:\Private\Data\FE\Opgave\navne_hash.txt");
            uint[] navne_hash_parsed = new uint[(int)navne.Length];
            for (int i = 0; i < navne.Length; i++)
            {
                var fields = navne[i].Split(',');
                navne_hash_parsed[i] = UInt32.Parse(navne[i]);
            }

            var tekst = File.ReadAllLines(@"C:\Private\Data\FE\Opgave\tekst_hash.txt");
            uint[] tekst_hash_parsed = new uint[(int)tekst.Length];
            for (int i = 0; i < tekst.Length; i++)
            {
                var fields = tekst[i].Split(',');
                tekst_hash_parsed[i] = UInt32.Parse(tekst[i]);
            }

            // Act
            uint[,] fundne_navne = new uint[navne.Length, 2];
            uint unikke_hits = 0;

            // Find alle forekomster af hashede navne i den hashede tekst og returner i array fundne_navne og unikke hits.
            SearchText.keywordSearch(navne_hash_parsed, tekst_hash_parsed, ref unikke_hits, ref fundne_navne);

            // Assert
            var result = File.ReadAllLines(@"C:\Private\Data\FE\Opgave\search_result.txt");
            uint[,] result_parsed = new uint[(int)result.Length / 2, 2];
            int resultIdx = 0;
            while (resultIdx < result.Length / 2 - 1)
            {
                result_parsed[resultIdx, 0] = UInt32.Parse(result[resultIdx * 2]);
                result_parsed[resultIdx, 1] = UInt32.Parse(result[resultIdx * 2 + 1]);
                resultIdx++;
            }
            CollectionAssert.AreEqual(result_parsed, fundne_navne);
        }
    }
}