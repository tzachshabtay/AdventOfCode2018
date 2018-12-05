using System;
using System.IO;
using System.Linq;

namespace Advent2018
{
    public static class Day5
    {
        public static void Run()
        {
            string polimer = File.ReadAllText("Input/Day5.txt");
            part2(polimer);
        }

        private static void part1(string polimer)
        {
            Console.WriteLine(react(polimer));
        }

        private static void part2(string polimer)
        {
            var result = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Min(c => react(polimer.Replace(c.ToString(), "").Replace(char.ToLowerInvariant(c).ToString(), "")));
            Console.WriteLine(result);
        }

        private static int react(string polimer)
        {
            string previous = null;
            while (previous != polimer)
            {
                previous = polimer;
                polimer = process(polimer);
            }
            return polimer.Length;
        }

        private static string process(string polimer)
        {
            for (int i = 0; i < polimer.Length - 1; i++)
            {
                if (arePolar(polimer[i], polimer[i + 1]))
                {
                    return polimer.Substring(0, i) + polimer.Substring(i + 2);
                }
            }
            return polimer;
        }

        private static bool arePolar(char c1, char c2)
        {
            return char.ToLowerInvariant(c1) == char.ToLowerInvariant(c2) && c1 != c2;
        }
    }
}
