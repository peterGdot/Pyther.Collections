namespace Pyther.Collections.Generic.Algorithms
{
    public static class BFS
    {
        /// <summary>
        /// Run Bread-First Search on a (directed) graph.
        ///   Time Complexity: O(N + E)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph to operate on (directed or undirected).</param>
        /// <param name="action">The action to execute, when the node is visited.</param>
        /// <param name="startNode">Optional start node. If set, the result will be limited to the subtree with this node as "root" node.</param>
        public static void Run<T>(Graph<T> graph, Action<T> action, GraphNode<T>? startNode = null)
        {
            bool[] visited = new bool[graph.Order];

            int nStart = startNode == null ? 0 : startNode.Index;
            int nStop  = startNode == null ? graph.Order - 1 : startNode.Index;

            for (int n = nStart; n <= nStop; n++)
            {
                if (!visited[n])
                {
                    visited[n] = true;

                    Queue<GraphNode<T>> queue = new();
                    queue.Enqueue(graph[n]!);
                    while (queue.Count > 0)
                    {
                        GraphNode<T> current = queue.Dequeue();
                        action?.Invoke(current.Data);
                        foreach (var adj in current.Neighbors)
                        {
                            if (!visited[adj.Index])
                            {
                                visited[adj.Index] = true;
                                queue.Enqueue(adj);
                            }
                        }
                    }
                }
            }
        }
    }
}
