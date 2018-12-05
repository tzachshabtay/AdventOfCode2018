using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018
{
    public static class Day1
    {
        public static void Run()
        {
            string[] input = Input.Get(1);
            part2(input);
        }

        private static void part1(string[] lines)
        {
            Console.WriteLine(lines.Sum(l => int.Parse(l.Replace(" ", ""))));
        }

        private static void part2(string[] lines)
        {
            HashSet<int> freqs = new HashSet<int>(100000);
            int freq = 0;
            while (true)
            {
                foreach (var line in lines)
                {
                    if (!freqs.Add(freq))
                    {
                        Console.WriteLine(freq);
                        return;
                    }
                    int addition = int.Parse(line.Replace(" ", ""));
                    freq += addition;
                }
            }
        }
    }
}
