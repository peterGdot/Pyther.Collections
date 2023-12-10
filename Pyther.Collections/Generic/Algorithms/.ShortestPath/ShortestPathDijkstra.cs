using Priority_Queue;
using Pyther.Collections.Generic.Graph;

namespace Pyther.Collections.Generic.Algorithms
{
    static class ShortestPathDijkstra
    {
        /// <summary>
        /// Find the shortest path between the source and target node based on edge weights.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph instance.</param>
        /// <param name="source">The source node.</param>
        /// <param name="target">The target node.</param>
        /// <returns>A list of all edges between the source and the target node or `null` if no path was found.</returns>
        public static List<GraphEdge<T>>? Run<T>(Graph<T> graph, GraphNode<T> source, GraphNode<T> target)
        {
            // current minimum distances to reach the given node
            double[] distances = new double[graph.Order];
            Array.Fill(distances, double.MaxValue);
            distances[source.Index] = 0.0;

            // array of indices of previous nodes
            int[] previous = new int[graph.Order];
            Array.Fill(previous, -1);

            // the priority queue
            // it's performance ist crucial for dijkstra
            SimplePriorityQueue<GraphNode<T>, double> priorityQueue = new();
            for (int i = 0; i < graph.Order; i++)
            {
                priorityQueue.Enqueue(graph[i]!, distances[i]);
            }

            // as long as we have potential candidates
            while (priorityQueue.Count != 0)
            {
                // pick candidate
                GraphNode<T> node = priorityQueue.Dequeue();
                // cheack all neighbors
                for (int i = 0; i < node.Neighbors.Count; i++)
                {
                    GraphNode<T> adj = node.Neighbors[i];
                    double weight = node.GetWeight(i);
                    double totalWeight = distances[node.Index] + weight;
                    if (distances[adj.Index] > totalWeight)
                    {
                        distances[adj.Index] = totalWeight;
                        previous[adj.Index] = node.Index;
                        // node is target node? => break
                        if (adj.Index == target.Index)
                        {
                            priorityQueue.Clear();
                            break;
                        }
                        priorityQueue.UpdatePriority(adj, distances[adj.Index]);
                    }
                }
            }
            // if no node leads to the target, we have no path
            if (previous[target.Index] == -1)
            {
                return null;
            }

            // create list of node indices from target to source and reverse
            List<int> indices = new List<int>();
            int index = target.Index;
            while (index >= 0)
            {
                indices.Add(index);
                index = previous[index];
            }
            indices.Reverse();

            // create final path as list of edges
            List<GraphEdge<T>> edges = new(indices.Count - 1);
            for (int i = 0; i < indices.Count - 1; i++)
            {
                edges.Add(graph[indices[i], indices[i + 1]]!);
            }

            return edges;
        }
    }
}
