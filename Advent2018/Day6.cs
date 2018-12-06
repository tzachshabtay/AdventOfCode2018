using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018
{
    public static class Day6
    {
        public static void Run()
        {
            string[] lines = Input.Get(6);
            var coords = lines.Select(l =>
            {
                string[] tokens = l.Split(",");
                return (int.Parse(tokens[0]), int.Parse(tokens[1]));
            });
            part2(coords.ToArray());
        }

        private static void part1((int x, int y)[] coords)
        {
            var maxX = coords.Max(c => c.x);
            var maxY = coords.Max(c => c.y);
            Square[,] matrice = new Square[maxX * 2 + 1, maxY * 2 + 1];
            int offsetX = maxX;
            int offsetY = maxY;
            int index = 1;
            List<Square> squares = new List<Square>(coords.Length);
            foreach (var (x, y) in coords)
            {
                var square = new Square { Index = index, Distance = 0, X = x + offsetX, Y = y + offsetY };;
                matrice[x + offsetX, y + offsetY] = square;
                squares.Add(square);
                index++;
            }
            while (squares.Count > 0) 
            {
                List<Square> newSquares = new List<Square>(squares.Count * 8);
                foreach (var square in squares)
                {
                    var x = square.X;
                    var y = square.Y;
                    fill(matrice, newSquares, square, x - 1, y);
                    fill(matrice, newSquares, square, x + 1, y);
                    fill(matrice, newSquares, square, x, y - 1);
                    fill(matrice, newSquares, square, x, y + 1);
                }
                squares = newSquares;
            }
            Dictionary<int, int> results = new Dictionary<int, int>();
            for (int candidate = 1; candidate < coords.Length + 1; candidate++)
            {
                if (inEdge(candidate, matrice)) continue;
                results[candidate] = getResult(candidate, matrice);
            }
            Console.WriteLine(results.Values.Max());
            //printMatrix(matrice);
        }

        private static void printMatrix(Square[,] matrice)
        {
            for (int y = 0; y < matrice.GetLength(1); y++)
            {
                for (int x = 0; x < matrice.GetLength(0); x++)
                {
                    if (matrice[x, y].Index == -1)
                    {
                        Console.Write(".");
                        continue;
                    }
                    char c = (char)(((byte)'a') + (byte)(matrice[x, y].Index - 1));
                    if (matrice[x, y].Distance == 0) Console.Write(char.ToUpperInvariant(c));
                    else Console.Write(c);
                }
                Console.WriteLine();
            }
        }

        private static int getResult(int index, Square[,] matrice)
        {
            int count = 0;
            for (int x = 0; x < matrice.GetLength(0); x++)
            {
                for (int y = 0; y < matrice.GetLength(1); y++)
                {
                    if (matrice[x, y].Index == index) count++;
                }
            }
            return count;
        }

        private static bool inEdge(int index, Square[,] matrice)
        {
            for (int x = 0; x < matrice.GetLength(0); x++)
            {
                if (matrice[x, 0].Index == index) return true;
                if (matrice[x, matrice.GetLength(1) - 1].Index == index) return true;
            }
            for (int y = 0; y < matrice.GetLength(1); y++)
            {
                if (matrice[0, y].Index == index) return true;
                if (matrice[matrice.GetLength(0) - 1, y].Index == index) return true;
            }
            return false;
        }

        private static void fill(Square[,] matrice, List<Square> squares, Square square, int x, int y)
        {
            int index = square.Index;
            if (x < 0 || y < 0) return;
            if (matrice.GetLength(0) <= x) return;
            if (matrice.GetLength(1) <= y) return;
            var existing = matrice[x, y];
            if (existing == null)
            {
                existing = new Square { X = x, Y = y, Distance = square.Distance + 1, Index = square.Index };
                matrice[x, y] = existing;
                squares.Add(existing);
                return;
            }
            if (existing.Index == index) return;
            if (existing.Index == -1) return;
            if (existing.Distance == square.Distance + 1) existing.Index = -1;               
        }

        private class Square
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Index { get; set; }
            public int Distance { get; set; }
        }

        private static void part2((int x, int y)[] coords)
        {
            var maxX = coords.Max(c => c.x);
            var maxY = coords.Max(c => c.y);
            int count = 0;
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var sumDist = getSumDistance(coords, x, y);
                    if (sumDist < 10000)
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        private static int getSumDistance((int x, int y)[] coords, int x, int y)
        {
            return coords.Sum(c => getDistance(c, x, y));
        }

        private static int getDistance((int x, int y) coord, int x, int y)
        {
            return Math.Abs(coord.x - x) + Math.Abs(coord.y - y);
        }
    }
}
