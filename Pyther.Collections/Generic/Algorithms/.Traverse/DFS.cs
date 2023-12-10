namespace Pyther.Collections.Generic.Algorithms
{
    public static class DFS
    {
        /// <summary>
        /// Run Depth-First Search on a (directed) acyclic graph.
        ///   Time Complexity: O(N + E)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph to operate on (directed or undirected).</param>
        /// <param name="action">The action to execute, when the node is visited.</param>
        /// <param name="startNode">Optional start node. If set, the result will be limited to the subtree with this node as "root" node.</param>
        public static void Run<T>(Graph<T> graph, Action<T> action, GraphNode<T>? startNode = null)
        {
            bool[] visibility = new bool[graph.Order];
            
            int nStart = startNode == null ? 0 : startNode.Index;
            int nStop = startNode == null ? graph.Order - 1 : startNode.Index;

            for (int n = nStart; n <= nStop; n++)
            {
                if (!visibility[n])
                {
                    DFSVisit(graph[n]!, ref visibility, action);
                }
            }
        }

        private static void DFSVisit<T>(GraphNode<T> node, ref bool[] visibility, Action<T> action)
        {
            visibility[node.Index] = true;
            foreach (var adj in node.Neighbors)
            {
                if (!visibility[adj.Index])
                {
                    DFSVisit(adj, ref visibility, action);
                }
            }
            action?.Invoke(node.Data);
        }
    }
}
