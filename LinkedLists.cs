using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOfAlgorithms
{
    class LinkedLists
    {

        public class ListNode<T>
        {
            public T value { get; set; }
            public ListNode<T> next { get; set; }

            static void printNode(ListNode<T> node, string msg)
            {
                Console.WriteLine("Printing values for node " + msg);
                while (node != null)
                {
                    Console.Write(node.value);
                    Console.Write(" ");
                    node = node.next;
                }
                Console.WriteLine("");
            }
        }

        // P1: Remove K From List
        // Given Linked List remove all occurences of value k
        // Solve the task in O(n) time using O(1) additional space
        // https://app.codesignal.com/interview-practice/task/gX7NXPBrYThXZuanm/description
        public static ListNode<int> RemoveFromLinkedList(ListNode<int> l, int k)
        {
            // If the value is null don't modyify anything
            if (l != null)
            {
                // Otherwise check if value is equal to k
                if (l.value == k)
                {
                    // If it is set reference to the current node to its child and repeat process
                    l = RemoveFromLinkedList(l.next, k);
                }
                else
                {
                    // Otherwise check for k in all of the children nodes
                    l.next = RemoveFromLinkedList(l.next, k);
                }
            }
            return l;
        }


        // P2: Is linked list a palindrome.
        // Check if given linked list is palindrome.
        // Solve in O(n) time using O(1) addtional space.
        // https://app.codesignal.com/interview-practice/task/HmNvEkfFShPhREMn4/description
        public static bool IsListPalindrome_Long(ListNode<int> l)
        {
            // The approach here is to find the middle of the list. 
            // Having found the middle subsequently reverse the order of the nodes to the right of middle
            // Finally, simply compare values of two lists
            if (l == null)
            {
                return true;
            }

            ListNode<int> findMiddle(ListNode<int> n)
            {
                // Probably overcomplicating this algorithm. 
                // The approach is to have two pointers, one moving twice per iteration, another moving once.
                // This way, when fast pointer reaches end of linked list the slow one should be in the middle.
                // It gets more complicated due to different logic needes for even and odd length lists.
                // (For further comparison we want even length logic to be split in two, while odd ones 
                // to point towards one common node)
                ListNode<int> fast = n, slow = n, prev_slow = null;
                int num_nodes = 0;
                while (true)
                {
                    bool b_flag = false;
                    for (int i = 0; i <= 1; i++)
                    {
                        if (fast != null)
                        {
                            num_nodes++;
                            fast = fast.next;
                        }
                        else
                        {
                            b_flag = true;
                            break;
                        }
                    }
                    if (b_flag)
                    {
                        break;
                    }
                    prev_slow = slow;
                    slow = slow.next;
                }
                if (num_nodes % 2 == 0)
                {
                    prev_slow.next = null;
                }
                return slow;
            }

            ListNode<int> reverse(ListNode<int> node, ListNode<int> prev)
            {
                // Reverse the order of linked list recursively. 
                // Is shortened in second version of this function
                if (node == null)
                {
                    return node;
                }
                ListNode<int> tmp = node.next;
                node.next = prev;
                if (tmp == null)
                {
                    return node;
                }
                else
                {
                    return reverse(tmp, node);
                }
            }

            bool compare(ListNode<int> org, ListNode<int> end)
            {
                // Compares two linked lists. Implicitly assumes the lists have the same length
                // Given that assumption can be greatly simplified and is much simpler in other version
                while (true)
                {
                    if (org.value != end.value)
                    {
                        return false;
                    }
                    if (end.next == null || org.next == null)
                    {

                        return true;
                    }
                    else
                    {
                        org = org.next;
                        end = end.next;
                    }
                }
            }

            ListNode<int> mid = findMiddle(l);
            ListNode<int> endNode = reverse(mid, null);
            return compare(l, endNode);
        }

        public static bool IsListPalindrome_Simplified(ListNode<int> l)
        {
            if (l == null)
            {
                return true;
            }
            int CountNodes(ListNode<int> n)
            {
                // Count number of nodes in linked list
                int counter = 0;
                while (n != null)
                {
                    counter++;
                    n = n.next;
                }
                return counter;
            }

            ListNode<int> FindMiddle(ListNode<int> n, int l)
            {
                // Given number of nodes find the middle node
                // If the number of the nodes is even disconect two chains
                int counter = 0;
                ListNode<int> priorNode = null;
                while (counter != l / 2)
                {
                    counter++;
                    priorNode = n;
                    n = n.next;
                }
                if (l % 2 == 0)
                {
                    priorNode.next = null;
                }
                return n;
            }

            ListNode<int> ReverseList(ListNode<int> node, ListNode<int> prev)
            {
                // Reverse linked list. Implicit assumption that the list is not null
                ListNode<int> tmp = node.next;
                node.next = prev;
                return tmp == null ? node : ReverseList(tmp, node);
            }

            bool CompareLists(ListNode<int> org, ListNode<int> end)
            {
                // Compare two lists, implicit assumption that the lists are equal length.
                while (end != null && org.value == end.value)
                {
                    org = org.next;
                    end = end.next;
                }
                return end == null;
            }
            ListNode<int> midNode = FindMiddle(l, CountNodes(l));
            ListNode<int> endNode = ReverseList(midNode, null);
            return CompareLists(l, endNode);
        }

        // P3: Add two huge numbers.
        // Given two huge integers represented as linked lists with each link representing number from 0 to 9999 find their sum
        // https://app.codesignal.com/interview-practice/task/RvDFbsNC3Xn7pnQfH/description
        public static ListNode<int> AddHugeNumbers(ListNode<int> a, ListNode<int> b)
        {
            // Given that linked lists are of unknown length, and MSB is on the left,
            // We need to firstly make sure that lists are alligned. 
            // After that is achieved we can add linked lists one cell at the time.
            // At the end we need to reverse the resulting list.
            if (a == null) return b;
            if (b == null) return a;

            (int sum, int overflow) CalculateSum(List<int> numbers)
            {
                return (numbers.Sum() % 10000, numbers.Sum() / 10000);
            }

            ListNode<int> ReverseList(ListNode<int> node, ListNode<int> prev)
            {
                ListNode<int> tmp = node.next;
                node.next = prev;
                return tmp == null ? node : ReverseList(tmp, node);
            }

            int CountNodes(ListNode<int> n)
            {
                int counter = 0;
                while (n != null)
                {
                    counter++;
                    n = n.next;
                }
                return counter;
            }

            ListNode<int> largeNum = ReverseList(a, null);
            ListNode<int> smallNum = ReverseList(b, null);

            if (CountNodes(smallNum) > CountNodes(largeNum))
            {
                ListNode<int> swapNode = largeNum;
                largeNum = smallNum;
                smallNum = swapNode;
            }

            ListNode<int> largeNumStart = largeNum;
            (int sum, int overflow) f_sum = (0, 0);
            while (true)
            {
                // Addition is performed in place, with largeNum being our return value
                int smallAdd = smallNum == null ? 0 : smallNum.value;
                f_sum = CalculateSum(new List<int>() { largeNum.value, smallAdd, f_sum.overflow });
                largeNum.value = f_sum.sum;

                if (largeNum.next == null)
                {
                    if (f_sum.overflow != 0)
                    {
                        ListNode<int> newNode = new ListNode<int>();
                        largeNum.next = newNode;
                        largeNum = newNode;
                        largeNum.value = f_sum.overflow;
                    }
                    return ReverseList(largeNumStart, null);
                }
                else
                {
                    smallNum = smallNum?.next;
                    largeNum = largeNum?.next;

                }
            }
        }


        // P4: Merge two Linked Lists.
        // Given two linked lists sorted in non-decreasing order, merge them.
        // Your solution should have O(l1.length + l2.length) time complexity
        // https://app.codesignal.com/interview-practice/task/RvDFbsNC3Xn7pnQfH/description
        public static ListNode<int> MergeLinkedList_Normal(ListNode<int> small, ListNode<int> large)
        {
            void ConsumeElem(ref ListNode<int> eater, ref ListNode<int> food)
            {
                eater.next = food;
                eater = eater.next;
                food = food.next;
            }

            if (small == null) return large;
            if (large == null) return small;
            ListNode<int> mergedListHead = new ListNode<int>();
            ListNode<int> mergedList = mergedListHead;
            while (true)
            {
                if (small.value >= large.value)
                {
                    ListNode<int> tmp = large;
                    large = small;
                    small = tmp;
                }
                ConsumeElem(ref mergedList, ref small);
                if (small == null)
                {
                    mergedList.next = large;
                    return mergedListHead.next;
                }
            }
        }

        public static ListNode<int> MergeLinkedList_Reccursive(ListNode<int> small, ListNode<int> large)
        {
            // Reccursively consumes nodes from smaller of linked lists
            if (small == null) return large;
            if (large == null) return small;

            if (small.value >= large.value)
            {
                ListNode<int> tmp = small;
                small = large;
                large = tmp;
            }
            small.next = MergeLinkedList_Reccursive(small.next, large);
            return small;
        }


        ListNode<int> ReverseNodesInGroups_Simple(ListNode<int> l, int k)
        {
            if (l == null || l.next == null || k == 1)
            {
                return l;
            }

            int CountNodes(ListNode<int> n)
            {
                int counter = 0;
                while (n != null)
                {
                    counter++;
                    n = n.next;
                }
                return counter;
            }
            Console.WriteLine(CountNodes(l));
            int cycles = CountNodes(l) / k;
            bool findGlobalHead = true;
            int swapCounter = k;
            ListNode<int> tmp;
            ListNode<int> prev = null;
            ListNode<int> segmentTail = l;
            ListNode<int> prevTail = null;
            ListNode<int> globalHead = null;
            while (true)
            {
                swapCounter--;
                if (swapCounter == 0)
                {
                    if (findGlobalHead)
                    {
                        globalHead = l;
                        findGlobalHead = false;
                    }
                    else
                    {
                        prevTail.next = l;
                    }
                    tmp = l.next;
                    l.next = prev;
                    segmentTail.next = tmp;
                    prev = null;
                    prevTail = segmentTail;
                    segmentTail = tmp;

                    l = tmp;

                    cycles--;
                    swapCounter = k;
                    if (cycles == 0)
                    {
                        break;
                    }
                }
                else
                {
                    tmp = l.next;
                    l.next = prev;
                    prev = l;
                    l = tmp;
                }
            }

            return globalHead;
        }

        ListNode<int> ReverseNodesInGroups_Partial(ListNode<int> l, int k)
        {
            if (l == null || l.next == null || k == 1)
            {
                return l;
            }

            void ReverseList(ListNode<int> firstNode, ListNode<int> lastNode, ListNode<int> prevNode, ListNode<int> nextNode)
            {
                if(prevNode != null)
                {
                    prevNode.next = lastNode;
                }
                ListNode<int> tmp = firstNode.next;
                ListNode<int> prev = firstNode;
                firstNode.next = nextNode;

                while(true)
                {
                    ListNode<int> swap = tmp.next;
                    tmp.next = prev;
                    prev = tmp;
                    tmp = swap;
                    if(prev == lastNode)
                    {
                        break;
                    }
                }
            }

            ListNode<int> globalHead = null;
            int counter = k;
            while(true)
            {
                
                l = l.next;

            }

            return globalHead;
        }


        /// Tmp
        int solution(string[][] classes) {
            Node.nodes = new List<Node>();
            foreach(var clas in classes)
            {
                new Node();
            }
            
            for(int i=0; i<classes.Count();i++)
            {
                for(int j=i+1;j<classes.Count();j++)
                {
                    foreach(string animal in classes[i])
                    {
                        if(classes[j].Contains(animal))
                        {
                            Node.nodes[i].edges.Add(Node.nodes[j]);
                            Node.nodes[j].edges.Add(Node.nodes[i]);    
                        }
                    }
                }
            }
            foreach (var node in Node.nodes)
        {
            Console.WriteLine(node.edges.Count);
        }
        int maxEdge = Node.nodes.Select(_ => _.edges.Count).OrderByDescending(_ => _).First();
        if (maxEdge == 0)
        {
            return 1;
        }
        if (maxEdge == 1)
        {
            return 2;
        }
        
        // Simplify alone nodes and single connected nodes
        List<Node> toDelete = Node.nodes.Where(_ => _.edges.Count < 2).ToList();
        while (toDelete.Count > 0)
        {
            foreach (var node in toDelete)
            {
                foreach (var neigh in node.edges)
                {
                    neigh.edges.Remove(node);
                }
                Node.nodes.Remove(node);
            }
            toDelete = Node.nodes.Where(_ => _.edges.Count < 2).ToList();
        }
        
        Console.WriteLine("After first prunning");
        foreach (var node in Node.nodes)
        {
            Console.WriteLine(node.edges.Count);
        }
        List<Node> dobuleEdges = Node.nodes.Where(_ => _.edges.Count == 2).ToList();
        while (true)
        {
            bool deleted = false;
            foreach (var node in dobuleEdges)
            {
                List<Node> neighs = node.edges.ToList();
                if (!neighs[0].edges.Contains(neighs[1]))
                {
                    neighs[0].edges.Remove(node);
                    neighs[1].edges.Remove(node);
                    neighs[0].edges.Add(neighs[1]);
                    neighs[1].edges.Add(neighs[0]);
                    Node.nodes.Remove(node);
                    deleted = true;
                }
        
        
        
            }
            if (!deleted)
            {
                break;
            }
            dobuleEdges = Node.nodes.Where(_ => _.edges.Count < 2).ToList();
        }
        
        Console.WriteLine("After second prunning");
        foreach (var node in Node.nodes)
        {
            Console.WriteLine(node.edges.Count);
        }
        
        int newMaxEdge = Node.nodes.Select(_ => _.edges.Count).OrderByDescending(_ => _).First();
        if (newMaxEdge >= 5)
        {
            return -1;
        }
        return maxEdge + 1;
        
        return 0;
        }
        
        
        class Node
        {
            public HashSet<Node> edges = new HashSet<Node>();
            public static List<Node> nodes = new List<Node>();
        
            public Node()
            {
                nodes.Add(this);
            }
        }

    }
}
