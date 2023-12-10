namespace Pyther.Collections.Generic.Algorithms
{
    public static  class TopologicalSortKahn
    {
        /// <summary>
        /// Run toplogical sort on a directed graph using Kahn's Algoritmus.
        ///   Time Complexity: O(N + E)
        ///   Auxiliary Space: O(N)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public static List<T> Run<T>(DirectedGraph<T> graph, bool reverse = false)
        {
            var result = new List<T>();

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
                result.Add(node.Data);

                foreach (GraphNode<T> adj in node.Neighbors)
                {
                    indegree[adj.Index]--;
                    if (indegree[adj.Index] == 0)
                    {
                        queue.Enqueue(adj);
                    }
                }
            }

            if (reverse)
            {
                result.Reverse();
            }

            return result;
        }
    }
}
