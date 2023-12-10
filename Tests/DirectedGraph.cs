using Pyther.Collections.Generic;

namespace Tests;

[TestClass]
public class DirectedGraph
{
    [TestMethod]
    public void Properties()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);

        Assert.AreEqual(graph.Order, 7);
        Assert.AreEqual(graph.Size, 8);
        Assert.AreEqual(graph.IsEmpty, false);
        Assert.AreEqual(graph.IsDirected, true);
        Assert.AreEqual(graph.IsWeighted, false);
        Assert.AreEqual(graph.AllowSelfLoops, false);
        Assert.AreEqual(graph.AllowDoubleNodes, false);
        Assert.AreEqual(graph.IsComplete, false);
        Assert.AreEqual(graph.IsTrivialGraph, false);
        Assert.AreEqual(graph.IsNullGraph, false);

        var dfsResult = new Queue<int>(new int[] { 0, 1, 3, 2, 4, 5, 6 });
        graph.DFS(node => {
            var a = node;
            var b = dfsResult.Dequeue();
            Assert.AreEqual(a, b);
        });

        var dfsResultWithStartNode = new Queue<int>(new int[] { 0, 1, 3, 2, 5 });
        graph.DFS(node => {
            var a = node;
            var b = dfsResultWithStartNode.Dequeue();
            Assert.AreEqual(a, b);
        }, 5);
    }

    [TestMethod]
    public void DFS()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);

        var dfsResult = new Queue<int>(new int[] { 0, 1, 3, 2, 4, 5, 6 });
        graph.DFS(node => {
            var a = node;
            var b = dfsResult.Dequeue();
            Assert.AreEqual(a, b);
        });

        var dfsResultWithStartNode = new Queue<int>(new int[] { 0, 1, 3, 2, 5 });
        graph.DFS(node => {
            var a = node;
            var b = dfsResultWithStartNode.Dequeue();
            Assert.AreEqual(a, b);
        }, 5);
    }

    [TestMethod]
    public void BFS()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);


        var bfsResult = new Queue<int>(new int[] { 0, 1, 2, 3, 4, 5, 6 });
        graph.BFS(node => {
            var a = node;
            var b = bfsResult.Dequeue();
            Assert.AreEqual(a, b);
        });

        var bfsResultWithStartNode = new Queue<int>(new int[] { 5, 0, 2, 3, 1 });
        graph.BFS(node => {
            var a = node;
            var b = bfsResultWithStartNode.Dequeue();
            Assert.AreEqual(a, b);
        }, 5);
    }

    [TestMethod]
    public void TopologicalSortKahn()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);

        var result = new Queue<int>(new int[] { 4, 5, 6, 2, 3, 1, 0 });
        
        foreach (var node in graph.TopologicalSort(TopologicalSortAlgorithm.Kahn))
        {
            var a = node;
            var b = result.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    [TestMethod]
    public void TopologicalSortDFS()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);

        var result = new Queue<int>(new int[] { 6, 5, 4, 2, 3, 1, 0 });

        foreach (var node in graph.TopologicalSort(TopologicalSortAlgorithm.DepthFirstSearch))
        {
            var a = node;
            var b = result.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    [TestMethod]
    public void ShortestPathDijkstra()
    {
        var graph = new DirectedGraph<int>();

        // Add Nodes
        for (int n = 0; n < 7; n++)
        {
            graph.Add(n);
        }

        // Add Edges
        graph.AddEdge(5, 0);
        graph.AddEdge(5, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 1);
        graph.AddEdge(4, 1);
        graph.AddEdge(4, 0);
        graph.AddEdge(1, 0);
        graph.AddEdge(2, 0);

        var result = new Queue<int>(new int[] { 5, 2, 2, 3, 3, 1 });
        var path = graph.ShortestPath(5, 1, ShortestPathAlgorithm.Dijkstra);

        Assert.AreNotEqual(path, null);
        foreach (var edge in path!)
        {
            var a = edge.Source.Data;
            var b = result.Dequeue();
            Assert.AreEqual(a, b);

            a = edge.Target.Data;
            b = result.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    [TestMethod]
    public void IsCyclic()
    {
        var graph = new DirectedGraph<int>();
        for (int n = 0; n <= 3; n++)
        {
            graph.Add(n);
        }
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 0);
        graph.AddEdge(2, 3);

        // DFS
        Assert.AreEqual(graph.IsCyclic(IsCyclicAlgorithm.DepthFirstSearch), true);
        // Kahn
        Assert.AreEqual(graph.IsCyclic(IsCyclicAlgorithm.Kahn), true);
    }

}
