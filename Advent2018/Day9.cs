using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2018
{
    public static class Day9
    {
        public static void Run()
        {
            part1(425, (70848) + 1);
        }

        private static void part1(int numPlayers, int numMarbles)
        {
            Dictionary<int, int> scores = new Dictionary<int, int>(numPlayers);
            List<int> marbles = new List<int>(numMarbles) { 0 };
            int currentMarble = 0;
            int currentPlayer = 0;
            for (int i = 1; i < numMarbles; i++)
            {
                var player = (i - 1) % numPlayers;
                currentPlayer = player;

                if (i % 23 == 0)
                {
                    scores.TryGetValue(player, out int score);
                    score += i;
                    var toRemove = clockwise(-7, currentMarble, marbles.Count);
                    if (toRemove < 0)
                    {
                        toRemove = marbles.Count + toRemove;
                    }
                    score += marbles[toRemove];
                    marbles.RemoveAt(toRemove);
                    scores[player] = score;
                    currentMarble = toRemove;
                }
                else
                {
                    currentMarble = clockwise(1, currentMarble, marbles.Count) + 1;
                    marbles.Insert(currentMarble, i);
                }
                //printMarbles(currentPlayer, marbles, currentMarble);
            }
            Console.WriteLine(scores.Values.Max());
        }

        private static int clockwise(int step, int current, int num)
        {
            return (current + step) % num;
        }

        private static void printMarbles(int player, List<int> marbles, int current)
        {
            Console.Write($"{player + 1}: ");
            for (int i = 0; i < marbles.Count; i++)
            {
                if (i == current) Console.Write($"({marbles[i]}) ");
                else Console.Write($"{marbles[i]} ");
            }
            Console.WriteLine();
        }
    }
}
