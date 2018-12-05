using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent2018
{
    public static class Day2
    {
        public static void Run()
        {
            string[] input = Input.Get(2);
            part2(input);
        }

        private static void part1(string[] lines)
        {
            int count2 = 0;
            int count3 = 0;
            foreach (string line in lines)
            {
                (int a, int b) = word(line);
                count2 += a;
                count3 += b;
            }
            Console.WriteLine(count2 * count3);
        }

        private static (int a, int b) word(string line)
        {
            Dictionary<char, int> chars = new Dictionary<char, int>(line.Length);
            foreach (char c in line)
            {
                chars.TryGetValue(c, out int count);
                chars[c] = count + 1;
            }
            return ((chars.Values.Any(c => c == 2) ? 1 : 0, chars.Values.Any(c => c == 3) ? 1 : 0));
        }

        private static void part2(string[] lines)
        {
            for (int x = 1; x < lines.Length; x++)
            {
                for (int y = 0; y < x; y++)
                {
                    if (isSimilar(lines[x], lines[y])) return;
                }
            }
        }

        static StringBuilder sb = new StringBuilder();
        private static bool isSimilar(string line1, string line2)
        {
            bool foundMismatch = false;
            sb.Clear();
            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i] == line2[i]) sb.Append(line1[i]);
                else if (foundMismatch) return false;
                else foundMismatch = true;
            }
            Console.WriteLine(sb.ToString());
            return true;
        }
    }
}
