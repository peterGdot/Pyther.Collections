namespace Pyther.Collections.Generic.Algorithms
{
    public static class IsCyclicKahn
    {
        /// <summary>
        /// Run cycle test on a directed graph using Kahn's algorithm.
        ///   Time Complexity: O(N + E)
        ///   Auxiliary Space: O(N)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph to test against.</param>
        /// <returns>Return TRUE if the graph has cycles, FALSE otherwise.</returns>
        public static bool Run<T>(DirectedGraph<T> graph)
        {
            int result = 0;

            // amount of incoming edges per Node
            int[] indegree = new int[graph.Order];

            // for each u
            for (int n = 0; n < graph.Order; n++)
            {
                indegree[n] = graph[n]!.InDegree;
            }

            Queue<GraphNode<T>> queue = new();

            // Add all priorityQueue with indegree of 0 to the priorityQueue
            for (int n = 0; n < graph.Order; n++)
            {
                if (indegree[n] == 0)
                {
                    queue.Enqueue(graph[n]!);
                }
            }

            while (queue.Count > 0)
            {
                GraphNode<T> node = queue.Dequeue();
                result++;

                foreach (GraphNode<T> adj in node.Neighbors)
                {
                    indegree[adj.Index]--;
                    if (indegree[adj.Index] == 0)
                    {
                        queue.Enqueue(adj);
                    }
                }
            }

            return result != graph.Order;
        }

    }
}
