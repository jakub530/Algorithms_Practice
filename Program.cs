using System;
using static BookOfAlgorithms.Arrays;
using static BookOfAlgorithms.LinkedLists;
using static BookOfAlgorithms.TreesBasic;

namespace BookOfAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string[] parts = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            string[] words = new string[] { "my Sunday"};
            SubTree(words, parts);
            // Try to get value, get null if myNode doesn't exists
            // Reverse(node1, 2);
            //Console.Write("Debug");
        }
    }
}
