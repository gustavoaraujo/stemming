using System;
using ptstemmer;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PTStemmer
{
    class Program
    {
        static void Main(string[] args)
        {
            var frases = DividirEmFrases("Normalmente, os sorteios ocorrem às quartas e sábados. A probabilidade de vencer em cada concurso varia de acordo com o número de dezenas jogadas e do tipo de aposta realizada.");

            Console.WriteLine("Frases:");
            foreach (var frase in frases)
            {
                Console.WriteLine($"- {frase}");

                Console.WriteLine(" ");
                Console.WriteLine("---");
                Console.WriteLine(" ");

                Console.WriteLine("Palavras:");

                var fraseRadicais = "";

                foreach (var palavra in Tokenizar(frase))
                {
                    var stemming = Stemming(palavra);
                    var ehStopWord = stemming.Equals(palavra);
                    var stopword = ehStopWord ? "Sim" : "Não";

                    Console.WriteLine($"- {palavra} Radical: {stemming} É Stopword? { stopword }");

                    fraseRadicais += !ehStopWord ? $"{stemming} " : "";
                }

                Console.WriteLine(" ");
                Console.WriteLine("---");
                Console.WriteLine(" ");

                Console.WriteLine($"Frase somente com os radicais: \"{fraseRadicais}\"");

                Console.WriteLine(" ");
                Console.WriteLine("---");
                Console.WriteLine(" ");
            }

            Console.ReadKey();
        }

        public static List<string> DividirEmFrases(string paragrafo)
        {
            return Regex.Split(paragrafo, @"(?<=[\.!\?])\s+").ToList();
        }

        public static List<string> Tokenizar(string frase)
        {
            frase = RemoverPontos(frase);
            return frase.Split(' ').ToList();
        }

        private static string RemoverPontos(string frase)
        {
            return new string(frase.Where(c => !char.IsPunctuation(c)).ToArray());
        }

        public static string Stemming(string palavra)
        {
            Stemmer s = Stemmer.StemmerFactory(Stemmer.StemmerType.ORENGO);
            s.enableCaching(1000);
            return s.getWordStem(palavra);
        }
    }
}
