namespace Pyther.Collections.Generic.Algorithms
{
    public static class TopologicalSortDFS
    {
        public static List<T> Run<T>(DirectedGraph<T> graph, bool reverse = false)
        {
            var result = new List<T>();

            graph.DFS(e => result.Add(e));

            // reverse means reverse twice
            if (!reverse)
            {
                result.Reverse();
            }
            return result;
        }
    }
}
