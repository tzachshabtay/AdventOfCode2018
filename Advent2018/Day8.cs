using System;
using System.IO;
using System.Linq;

namespace Advent2018
{
    public static class Day8
    {
        public static void Run()
        {
            string[] tokens = File.ReadAllText("Input/Day8.txt").Split(" ");
            var root = buildTree(0, tokens);
            part2(root);
        }

        private static void part1(Node node)
        {
            Console.WriteLine(getChecksum(node));
        }

        private static int getChecksum(Node node)
        {
            int checksum = node.Metadata.Sum() + node.Children.Sum(c => getChecksum(c));
            return checksum;
        }

        private static void part2(Node node)
        {
            Console.WriteLine(getValue(node));
        }

        private static int getValue(Node node)
        {
            if (node.Children.Length == 0) return node.Metadata.Sum();
            return node.Metadata.Sum(m => m <= 0 || m > node.Children.Length ? 0 : getValue(node.Children[m - 1]));
        }

        private static Node buildTree(int startIndex, string[] tokens)
        {
            var numChildren = int.Parse(tokens[startIndex]);
            var numMetadata = int.Parse(tokens[startIndex + 1]);
            int childIndex = startIndex + 2;
            Node[] children = new Node[numChildren];
            for (int i = 0; i < numChildren; i++)
            {
                var child = buildTree(childIndex, tokens);
                children[i] = child;
                childIndex = child.NextIndex;
            }
            int[] metadata = new int[numMetadata];
            for (int i = 0; i < numMetadata; i++)
            {
                metadata[i] = int.Parse(tokens[childIndex]);
                childIndex++;
            }
            return new Node(metadata, children, childIndex);
        }

        private class Node
        {
            public Node(int[] metadata, Node[] children, int nextIndex)
            {
                Metadata = metadata;
                Children = children;
                NextIndex = nextIndex;
            }
            public int[] Metadata { get; }
            public Node[] Children { get; }
            public int NextIndex { get; }
        }
    }
}
