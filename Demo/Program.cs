using Pyther.Collections.Generic;
using Pyther.Collections.Generic.Tree;

internal class Program
{
    internal class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public override string? ToString()
        {
            return $"{Name} with the age of {Age}";
        }

        public int CompareTo(Person? other)
        {
            return this.Age.CompareTo(other?.Age ?? 0);
        }
    }

    private static void Main()
    {
        Console.WriteLine();
        Console.WriteLine("- Directed Graph ---------------------------------------------------------------");
        Console.WriteLine();

        var graph1 = new DirectedGraph<int>();

        for (int n = 0; n < 7; n++) {
            graph1.Add(n);
        }

        graph1.AddEdge(5, 0);
        graph1.AddEdge(5, 2);
        graph1.AddEdge(2, 3);
        graph1.AddEdge(3, 1);
        graph1.AddEdge(4, 1);
        graph1.AddEdge(4, 0);
        graph1.AddEdge(1, 0);
        graph1.AddEdge(2, 0);

        graph1.Log(s => Console.WriteLine(s));

        Console.WriteLine();
        Console.Write($"DFS (without start node){Environment.NewLine}  ");
        graph1.DFS(e => Console.Write(e.ToString() + ", "));
        Console.WriteLine();

        Console.Write($"DFS (with start node 5){Environment.NewLine}  ");
        graph1.DFS(e => Console.Write(e.ToString() + ", "), 5);
        Console.WriteLine();

        Console.WriteLine();
        Console.Write($"BFS (without start node){Environment.NewLine}  ");
        graph1.BFS(e => Console.Write(e.ToString() + ", "));
        Console.WriteLine();

        Console.Write($"BFS (with start node 5){Environment.NewLine}  ");
        graph1.BFS(e => Console.Write(e.ToString() + ", "), 5);
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("Topological Sort (Kahn's)");
        var list1 = graph1.TopologicalSort(TopologicalSortAlgorithm.Kahn);
        Console.WriteLine("  " + string.Join(", ", list1.Select(g => g.ToString())));

        Console.WriteLine("Topological Sort (DFS)");
        var list2 = graph1.TopologicalSort(TopologicalSortAlgorithm.DepthFirstSearch);
        Console.WriteLine("  " + string.Join(", ", list2.Select(g => g.ToString())));

        Console.WriteLine();
        Console.WriteLine("Shortest Path (Dijkstra)");
        var list3 = graph1.ShortestPath(5, 1, ShortestPathAlgorithm.Dijkstra);
        if (list3 != null)
        {
            Console.WriteLine("  " + string.Join($"{Environment.NewLine}  ", list3.Select(g => g.ToString())));
        } else
        {
            Console.WriteLine("  no path found!");
        }

        var graph2 = new DirectedGraph<int>();
        for (int n = 0; n <= 3; n++)
        {
            graph2.Add(n);
        }
        graph2.AddEdge(0, 1);
        graph2.AddEdge(0, 2);
        graph2.AddEdge(1, 2);
        graph2.AddEdge(2, 0);
        graph2.AddEdge(2, 3);

        Console.WriteLine();
        Console.WriteLine("IsCyclic (DFS)");
        Console.WriteLine("  " + graph2.IsCyclic(IsCyclicAlgorithm.DepthFirstSearch));
        Console.WriteLine("IsCyclic (Kahn)");
        Console.WriteLine("  " + graph2.IsCyclic(IsCyclicAlgorithm.Kahn));

        Console.WriteLine();
        Console.WriteLine("- BinaryTree -------------------------------------------------------------------");
        Console.WriteLine();

        var binTree = new BinaryTree<int>
        {
            Root = new BinaryTreeNode<int>(1)
        };
        binTree.Root.
            Add(2).
                Add(4).Up().
                Add(5).Up().
                Up().
            Add(3).
                Add(6).Up().
                Add(7);

        binTree.Root.Log(str => Console.WriteLine(str));

        Console.Write("Pre-Order Traversal:    "); 
        binTree.Root.Traverse(BinaryTreeTraversal.PreOrder, (node, _) => Console.Write(node.Data + ", "));
        Console.WriteLine();

        Console.Write("In-Order Traversal:     ");
        binTree.Root.Traverse(BinaryTreeTraversal.InOrder, (node, _) => Console.Write(node.Data + ", "));
        Console.WriteLine();

        Console.Write("Post-Order Traversal:   ");
        binTree.Root.Traverse(BinaryTreeTraversal.PostOrder, (node, _) => Console.Write(node.Data + ", "));
        Console.WriteLine();

        Console.Write("Level-Order Traversal:  ");
        binTree.Root.Traverse(BinaryTreeTraversal.LevelOrder, (node, _) => Console.Write(node.Data + ", "));
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("- MinHeap ----------------------------------------------------------------------");
        Console.WriteLine();
        var heap = new MinHeap<int>();
        heap.Insert(10);
        heap.Insert(15);
        heap.Insert(17);
        heap.Insert(6);
        heap.Insert(12);
        heap.Insert(7);
        heap.Log(str => Console.WriteLine(str));
        while (!heap.Empty)
        {
            Console.WriteLine(heap.Extract().ToString());
        }

        Console.WriteLine();
        Console.WriteLine("- MinHeap for objects -------------------------------------------------------------------");
        Console.WriteLine();

        //var heap2 = new MinHeap<Person>(Comparer<Person>.Create((a, b) => a.Age.CompareTo(b.Age)), new Person("", 0));
        var heap2 = new MinHeap<Person>(Comparer<Person>.Default, new Person("", int.MaxValue));
        var jane = new Person("Jane Doe", 45);
        heap2.Insert(new Person("John Doe", 42));
        heap2.Insert(jane);
        heap2.Insert(new Person("Max Müller", 28));
        heap2.Insert(new Person("Maxi Müller", 26));
        jane.Age = 27;
        heap2.Replace(jane, jane);

        heap2.Log(str => Console.WriteLine(str));
        while (!heap2.Empty)
        {
            Console.WriteLine(heap2.Extract().ToString());
        }

        Console.WriteLine();
        Console.WriteLine("- MaxHeap for objects -------------------------------------------------------------------");
        Console.WriteLine();

        //var heap2 = new MinHeap<Person>(Comparer<Person>.Create((a, b) => a.Age.CompareTo(b.Age)), new Person("", 0));
        var heap3 = new MaxHeap<Person>(Comparer<Person>.Default, new Person("", int.MaxValue));
        var jane2 = new Person("Jane Doe", 45);
        heap3.Insert(new Person("John Doe", 42));
        heap3.Insert(jane2);
        heap3.Insert(new Person("Max Müller", 28));
        heap3.Insert(new Person("Maxi Müller", 26));
        jane2.Age = 27;
        heap3.Replace(jane2, jane2, 45.CompareTo(jane2.Age));

        heap3.Log(str => Console.WriteLine(str));
        while (!heap3.Empty)
        {
            Console.WriteLine(heap3.Extract().ToString());
        }
    }
}