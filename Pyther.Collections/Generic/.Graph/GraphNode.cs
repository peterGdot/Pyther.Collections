namespace Pyther.Collections.Generic
{
    /// <summary>
    /// A generic Graph Node for (un)directed/(un)weighted graphs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphNode<T>
    {
        /// <summary>Node content.</summary>
        public T Data { get; set; }

        /// <summary>The index of this node in the Graph (-1 means this node is not part of a graph).</summary>
        public int Index { get; internal set; }

        /// <summary>List to all adjacent nodes.</summary>
        public List<GraphNode<T>> Neighbors { get; } = new List<GraphNode<T>>();

        /// <summary>Optional list of weights of all adjecent nodes.</summary>
        public List<double>? Weights { get; internal set; }

        /// <summary>The number of edges that goes out from the node.</summary>
        public int OutDegree => Neighbors.Count;

        /// <summary>The number of edges that coming in to the node.</summary>
        public int InDegree { get; internal set; }

        /// <summary>The number of all edges that goes out or coming in from/to the node.</summary>
        public int Degree => OutDegree + InDegree;

        /// <summary>Returns true, if this node is not connected to any other node..</summary>
        public bool IsIsolated => OutDegree == 0 && InDegree == 0;

        /// <summary>Returns true, if this node contain an edge pointing to itself.</summary>
        public bool HasSelfLoop => Neighbors.Contains(this);

        /// <summary>
        /// Create a new node instance of given inner data.
        /// </summary>
        /// <param name="data"></param>
        public GraphNode(T data)
        {
            Data = data;
            Index = -1;
        }

        /// <summary>
        /// Return the weights of an outgoing edge. For unweighted graphs/nodes, the result will always be 1.0 .
        /// </summary>
        /// <param name="indexEdge">The index of the edge in 0 .. Neighbors.Count - 1 range. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetWeight(int indexEdge)
        {
            if (indexEdge < 0 || indexEdge >= Neighbors.Count)
            {
                throw new ArgumentException($"Edge index {indexEdge} out of range [0..{Neighbors.Count - 1}]!", nameof(indexEdge));
            }
            return Weights != null ? Weights[indexEdge] : 1.0;
        } 

        /// <summary>
        /// Return the edge index of a given adjacent node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>The zero-based index of the first occurrence of the adjacent node. If the node was not dound -1 will be returned.</returns>
        public int EdgeIndexOf(GraphNode<T> node)
        {
            return Neighbors.IndexOf(node);
        }

        public override string ToString()
        {
            return $"Node<{Data}>: OutDegree = {OutDegree}, InDegree = {InDegree}, IsIsolated = {IsIsolated}";
        }
    }
}
