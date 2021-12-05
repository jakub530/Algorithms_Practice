using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfAlgorithms
{
    class TreesBasic
    {
        public static string[] SubTree(string[] words, string[] parts)
        {
            Tree tri = new Tree();

            foreach (var part in parts)
            {
                Tree tmp = tri;
                foreach (var c in part)
                {
                    if (!tmp.branches.ContainsKey(c))
                    {
                        tmp.branches.Add(c, new Tree());
                        tmp.branches[c].parent = tmp;
                        tmp.branches[c].val = c;
                    }
                    tmp = tmp.branches[c];
                }
                tmp.leaf = true;
            }

            foreach (var word in words)
            {
                string part = "";
                List<Tree> trees = new List<Tree>();
                foreach (char c in word)
                {
                    trees.Add(tri);
                    List<Tree> newTrees = new List<Tree>();
                    foreach (var tree in trees)
                    {
                        if (tree.branches.ContainsKey(c))
                        {
                            newTrees.Add(tree.branches[c]);
                            if (tree.branches[c].leaf)
                            {
                                string output = tree.reconstructString();
                                if (output.Length > part.Length)
                                {
                                    part = output;
                                }
                            }
                        }
                    }
                    trees = newTrees;
                    if (part.Length >= 5)
                    {
                        break;
                    }
                }
                Console.WriteLine($"For string {word} part is {part}");
            }



            return null;
        }

        public class Tree
        {
            public Dictionary<char, Tree> branches = new Dictionary<char, Tree>();
            public Tree parent = null;
            public char val;
            public bool leaf = false;

            public string reconstructString()
            {
                string output = "";
                Tree tmpTree = this;
                while (tmpTree.parent != null)
                {
                    output += tmpTree.val;
                    tmpTree = tmpTree.parent;
                }
                char[] charArray = output.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
        }


    }
}
