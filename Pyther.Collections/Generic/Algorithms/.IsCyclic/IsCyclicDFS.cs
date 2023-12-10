namespace Pyther.Collections.Generic.Algorithms
{
    public static class IsCyclicDFS
    {
        [Flags]
        enum MaskFlag : byte
        {
            None     = 0b00000000,
            Visited  = 0b00000001,  // is the node already visited?
            InPath   = 0b00000010   // is the node in the current dfs path/stack?
        }

        /// <summary>
        /// Run cycle test on a directed graph using Depth-First Search.
        ///   Time Complexity: O(N + E)
        ///   Auxiliary Space: O(N)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph to test against.</param>
        /// <param name="startNode">Optional start node if you want test a sub graph.</param>
        /// <returns>Return TRUE if the graph has cycles, FALSE otherwise.</returns>
        public static bool Run<T>(DirectedGraph<T> graph, GraphNode<T>? startNode = null)
        {
            MaskFlag[] mask = new MaskFlag[graph.Order];

            int nStart = startNode == null ? 0 : startNode.Index;
            int nStop = startNode == null ? graph.Order - 1 : startNode.Index;

            for (int n = nStart; n <= nStop; n++)
            {
                if (DFSVisit(graph[n]!, ref mask))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DFSVisit<T>(GraphNode<T> node, ref MaskFlag[] mask)
        {
            if (mask[node.Index].HasFlag(MaskFlag.InPath))
            {
                return true;
            }
            if (mask[node.Index].HasFlag(MaskFlag.Visited))
            {
                return false;
            }
            
            mask[node.Index] |= (MaskFlag.Visited | MaskFlag.InPath);

            foreach (var adj in node.Neighbors)
            {
                if (DFSVisit(adj, ref mask))
                {
                    return true;
                }
            }

            mask[node.Index] &= ~MaskFlag.InPath;
            return false;
        }

    }
}
