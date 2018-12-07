using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent2018
{
    public class Day7
    {
        public static void Run()
        {
            string[] input = Input.Get(7);
            part2(input.Select(i => new Edge(i)).ToList());
        }

        private static void part1(List<Edge> edges)
        {
            StringBuilder sb = new StringBuilder();
            HashSet<char> remaining = new HashSet<char>();
            foreach (var edge in edges)
            {
                remaining.Add(edge.From);
                remaining.Add(edge.To);
            }

            char? next = null;
            do
            {
                List<char> candidates = getCandidates(edges, remaining);
                next = getNext(candidates);
                if (next != null)
                {
                    sb.Append(next.Value);
                    remaining.Remove(next.Value);
                    edges.RemoveAll(e => e.From == next.Value);
                }
            } while (next != null);
            Console.WriteLine(sb.ToString());
        }

        private static char? getNext(List<char> candidates)
        {
            if (candidates.Count == 0) return null;
            return candidates.Min();
        }

        private static List<char> getCandidates(List<Edge> edges, HashSet<char> remaining)
        {
            return remaining.Where(r => edges.All(e => e.To != r)).ToList();
        }

        private class Edge
        {
            public Edge(string line)
            {
                From = line[5];
                To = line[36];
            }

            public char From { get; }
            public char To { get; }
        }

        private static void part2(List<Edge> edges)
        {
            int time = -1;
            HashSet<char> remaining = new HashSet<char>();
            foreach (var edge in edges)
            {
                remaining.Add(edge.From);
                remaining.Add(edge.To);
            }

            var workers = new List<Worker>(numWorkers);
            for (int i = 0; i < numWorkers; i++) workers.Add(new Worker());

            while (remaining.Count > 0)
            {
                time++;
                foreach (var worker in workers)
                {
                    worker.TimeLeft--;
                    if (worker.TimeLeft <= 0)
                    {
                        edges.RemoveAll(e => e.From == worker.WorkingOn);
                        remaining.Remove(worker.WorkingOn);
                    }
                }
                while (assignNextWork(workers, remaining, edges)) {}                
            }
            Console.WriteLine(time);
        }

        private static bool assignNextWork(List<Worker> workers, HashSet<char> remaining, List<Edge> edges)
        {
            var availableWorker = workers.FirstOrDefault(w => w.TimeLeft <= 0);
            if (availableWorker == null) return false;           
            List<char> candidates = getCandidates(edges, remaining);
            candidates.RemoveAll(c => workers.Any(w => w.WorkingOn == c));

            char? next = getNext(candidates);
            if (next == null) return false;
            availableWorker.TimeLeft = getTime(next.Value);
            availableWorker.WorkingOn = next.Value;
            return true;
        }

        private class Worker
        {
            public int TimeLeft { get; set; }
            public char WorkingOn { get; set; }
        }

        private static int getTime(char c) => 61 + (c - 'A');

        private static int numWorkers = 5;
    }
}