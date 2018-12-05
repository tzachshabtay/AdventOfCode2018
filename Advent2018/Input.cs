using System;
using System.IO;

namespace Advent2018
{
    public static class Input
    {
        public static string[] Get(int day)
        {
            string filename = $"Input/Day{day}.txt";
            return File.ReadAllLines(filename);
        }
    }
}