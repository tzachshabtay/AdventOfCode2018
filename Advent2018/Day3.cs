using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018
{
    public static class Day3
    {
        public static void Run()
        {
            string[] lines = Input.Get(3);

            Claim[] claims = lines.Select(s => new Claim(s)).ToArray();
            part2(claims);
        }

        private static void part1(Claim[] claims)
        {
            int[,] fabric = new int[1000, 1000];
            int count = 0;
            foreach (var claim in claims)
            {
                for (int x = claim.X; x < claim.X + claim.Width; x++)
                {
                    for (int y = claim.Y; y < claim.Y + claim.Height; y++)
                    {
                        fabric[x, y] += 1;
                        if (fabric[x, y] == 2) count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        private static void part2(Claim[] claims)
        {
            List<Claim>[,] fabric = new List<Claim>[1000, 1000];
            foreach (var claim in claims)
            {
                for (int x = claim.X; x < claim.X + claim.Width; x++)
                {
                    for (int y = claim.Y; y < claim.Y + claim.Height; y++)
                    {
                        var existingClaims = fabric[x, y] ?? new List<Claim>();
                        existingClaims.Add(claim);
                        if (existingClaims.Count > 1)
                        {
                            foreach (var existing in existingClaims)
                            {
                                existing.Overlaps = true;
                            }
                        }
                        fabric[x, y] = existingClaims;
                    }
                }
            }
            Console.WriteLine(claims.First(c => !c.Overlaps).ID);
        }

        private class Claim
        {
            public Claim(string line)
            {
                //#1 @ 483,830: 24x18
                string[] tokens = line.Split(" ");
                ID = tokens[0];
                string[] xy = tokens[2].Split(",");
                X = int.Parse(xy[0]);
                Y = int.Parse(xy[1].Substring(0, xy[1].Length - 1));
                string[] wh = tokens[3].Split("x");
                Width = int.Parse(wh[0]);
                Height = int.Parse(wh[1]);
            }
            public string ID { get; }
            public int X { get; }
            public int Y { get; }
            public int Width { get; }
            public int Height { get; }
            public bool Overlaps { get; set; }
        }
    }
}
