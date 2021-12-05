using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfAlgorithms
{
    class Arrays
    {
        // P1:First Duplicate 
        // Returns item, for which second occurance has minimal index
        // if no such item exists return -1.
        // https://app.codesignal.com/interview-practice/task/pMvymcahZ8dY4g75q/description
        public static int FirtDuplicate_Hash(int[] a)
        {
            // Hashsets provide high performance set operations.
            // Set is a collection of items with no duplicates. 
            HashSet<int> occur = new();
            foreach (int el in a)
            {
                if (occur.Contains(el))
                {
                    return el;
                }
                else
                {
                    occur.Add(el);
                }
            }
            return -1;
        }


        // P2:First Not Repeating Character
        // Given a string s find and return first instance of non-repeating character.
        // If there is no such character return '_'
        // https://app.codesignal.com/interview-practice/task/uX5iLwhc6L5ckSyNC/description
        public static char FirstNotRepeatingCharacter_Generic(string s)
        {
            // Mostly generic solution. Firsty it creates a dictionary of occurences. 
            // Then it filters the dictionary, to only get items, which occur a signle time.
            // Finally it sorts it by first index (since order of elements in dictionary is non-deterministic).
            Dictionary<char, int> occur = new ();
            for (int i = 0; i < s.Length; i++)
            {
                if (occur.ContainsKey(s[i]))
                {
                    occur[s[i]] = -1;

                }
                else
                {
                    occur.Add(s[i], i);
                }
            }
            foreach (var elem in occur)
            {
                Console.WriteLine($"Key for {elem.Key} is {elem.Value}");
            }
            char x = occur.Where(elem => elem.Value != -1).OrderBy(elem => elem.Value).Select(_ => _.Key).FirstOrDefault();
           // char y;
            return x == (char)0 ? '_' : x;
        }

        public static char FirstNotRepeatingCharacter_Linq(string s)
        {
            // Full LINQ solution. It works, because groupby is automatically sorted by occurence.
            // After items are groupped it filters them by count, and selects first group.
            // Because Group is reference type if no group is found default value is null (unlike '\0' in case of char)
            var y = s.GroupBy(_ => _).Where(_ => _.Count() == 1).FirstOrDefault();
            return y == null ? '_' : y.Key;
        }


        // P3:2D Array Rotation
        // Rotate 2D array 90 degrees clockwise in place (using O(1) additional memory)
        // https://app.codesignal.com/interview-practice/task/5A8jwLGcEpTPyyjTB/description
        public static int[][] ArrayRotation_4_Corner(int[][] a)
        {
            // Takes each set of 4 corners and rotates them
            // Each subsequent iteration of outer loop
            // takes care of separate "ring" of values.
            int len = a.Length - 1;
            for (int i = 0; i < a.Length; i++)
            {
                int maxi = a.Length - 1 - i;
                for (int j = i; j < maxi; j++)
                {
                    int tmp = a[i][j];
                    a[i][j] = a[len - j][i];
                    a[len - j][i] = a[len - i][len - j];
                    a[len - i][len - j] = a[j][len - i];
                    a[j][len - i] = tmp;
                }
            }
            return a;
        }

        //P4:Sudoku Puzzle
        // Check if given 2D array constitues valid sudoku
        // Empty places are represented as an '.'
        // https://app.codesignal.com/interview-practice/task/SKZ45AF99NpbnvgTn/solutions
        public static bool SudokuPuzleValid_Simple(char[][] grid)
        {
            // We use a function to check if given list of numbers is distinct
            // If each row, each column and each 3x3 square is distinct the sudoku is valid
            static bool isDistinct(IEnumerable<char> input)
            {
                input = input.Where(_ => _ != '.');
                return input.Count() != input.Distinct().Count();
            }
            for (int i = 0; i < 9; i++)
            {
                if (isDistinct(grid[i]))
                {
                    return false;
                }
                if (isDistinct(grid.Select(elem => elem[i])))
                {
                    return false;
                }
                // Numbers for 3x3 Sectors are selected depending on value of i in following fashion:
                // 0 1 2
                // 3 4 5
                // 6 7 8
                // In order to achieve this rows are selected using division by 3 while columns using modulo 3
                if (isDistinct(grid.Where((_, index) => (i / 3) == index / 3).Select(_ => _.Where((_, index) => i % 3 == index / 3)).SelectMany(x => x)))
                {

                    return false;
                }
            }
            return true;
        }

        //P5:Is Crypt Solution
        // Given a Cryptography Key and 3 strings
        // Find if s[0] + s[1] == s[2] after decoding
        // If any number contains leading 0 return false
        // https://app.codesignal.com/interview-practice/task/yM4uWYeQTHzYewW9H/description
        public static bool IsCryptSolution(string[] crypt, char[][] solution)
        {
            // Mapping dictionary used for decoding 
            Dictionary<char, int> mapping = solution.ToDictionary(t => t[0], t => (int)t[1] - (int)'0');

            long Conv(string value)
            {
                // Convert string to int using mapping dictionary
                long tmp = value.Select((_, index) => (long)Math.Pow(10, value.Length - index - 1) * mapping[_]).Sum();
                // Special condition needed, otherwise number 0 would fail check for leading zeros
                if (value.Length == 1)
                {
                    return tmp;
                }
                // If number is smaller than it should be based upon string length it means leading zeros must be involved
                return tmp < Math.Pow(10, value.Length - 1) ? -1 : tmp;
            }

            return Conv(crypt[0]) + Conv(crypt[1]) == Conv(crypt[2]);
        }





    }

}
