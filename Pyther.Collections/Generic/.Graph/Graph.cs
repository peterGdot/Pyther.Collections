using Pyther.Collections.Generic.Algorithms;
using Pyther.Collections.Generic.Graph;

namespace Pyther.Collections.Generic
{
    public abstract class Graph<T>
    {
        /// <summary>List of all nodes inside the graph.</summary>
        protected readonly List<GraphNode<T>> nodes = new();

        // public List<GraphNode<T>> Nodes => nodes;

        /// <summary></summary>
        protected GraphFlags Flags { get; } = GraphFlags.None;

        public bool IsDirected { get; protected set; }


        /// <summary>Returns true, if the graphs edge have weights.</summary>
        public bool IsWeighted => Flags.HasFlag(GraphFlags.Weighted);
        public bool AllowSelfLoops => Flags.HasFlag(GraphFlags.AllowSelfLoops);
        public bool AllowDoubleNodes => Flags.HasFlag(GraphFlags.AllowDoubleNodes);

        /// <summary>The number of nodes/vertices in the graph.</summary>
        public int Order => nodes.Count;

        /// <summary>The number of edges in the graph.</summary>
        public int Size { get; protected set; }

        /// <summary>Is the graph empy (no nodes or edges)?</summary>
        public bool IsEmpty => Order == 0;
        /// <summary>Have each node an edge to all other nodes?</summary>
        public bool IsComplete => Size == (Order * (Order - 1)) / 2;
        /// <summary>Contain the graph only one node?</summary>
        public bool IsTrivialGraph => Order == 1;
        /// <summary>Doesn't the graph contain edges?</summary>
        public bool IsNullGraph => Size == 0;

        protected Graph(GraphFlags flags = GraphFlags.None)
        {
            this.Flags = flags;
        }

        #region Nodes

        public GraphNode<T>? this[int nodeIndex]
        {
            get
            {
                return nodes[nodeIndex];
            }
        }

        /// <summary>
        /// Add a new item as node to the graph.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>The new created GraphNode.</returns>
        public virtual GraphNode<T> Add(T item)
        {
            GraphNode<T>? node;
            if (!AllowDoubleNodes && (node = this.FindFirstNode(item)) != null)
            {
                return node;
            }
            node = new(item);
            if (this.IsWeighted)
            {
                node.Weights = new();
            }
            node.Index = nodes.Count;
            nodes.Add(node);
            return node;
        }

        /// <summary>
        /// Remove a node from the the graph.
        /// The node can only be removed, if it doesn't contain any edges.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual bool Remove(GraphNode<T> node)
        {
            if (node.Degree != 0)
            {
                return false;
            }
            var index = nodes.IndexOf(node);
            if (index >= 0)
            {
                nodes.RemoveAt(index);
                node.Index = -1;
                for (var i = index; i < nodes.Count; i++)
                {
                    nodes[i].Index--;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(T item)
        {
            GraphNode<T>? node = FindFirstNode(item);
            return node != null && Remove(node);
        }

        /// <summary>
        /// Clear all nodes and edges.
        /// </summary>
        /// <param name="UpdateNodes"></param>
        public void Clear(bool UpdateNodes = true)
        {
            if (UpdateNodes)
            {
                for (int n = 0; n < nodes.Count; n++)
                {
                    nodes[n].Neighbors.Clear();
                    nodes[n].InDegree = 0;
                    nodes[n].Index = -1;
                }
            }
            this.Size = 0;
            nodes.Clear();
        }

        public GraphNode<T>? FindFirstNode(T item)
        {
            foreach (var node in nodes)
            {
                if (node.Data != null && node.Data.Equals(item))
                {
                    return node;
                }
            }
            return null;
        }

        public int? FindFirstIndex(T item)
        {
            GraphNode<T>? node = FindFirstNode(item);
            return node != null ? node.Index : null;
        }

        public bool Contains(T item)
        {
            return FindFirstNode(item) != null;
        }

        public bool Contains(GraphNode<T> node)
        {
            return nodes.Contains(node);
        }


        #endregion

        #region Edges

        public GraphEdge<T>? this[int source, int target]
        {
            get
            {
                GraphNode<T> sourceNode = nodes[source];
                GraphNode<T> targetNode = nodes[target];
                int adjIndex = sourceNode.Neighbors.IndexOf(targetNode);
                if (adjIndex >= 0)
                {
                    return new GraphEdge<T>(
                        sourceNode,
                        targetNode,
                        sourceNode.GetWeight(adjIndex)
                    );
                }
                return null;
            }
        }

        public abstract bool AddEdge(GraphNode<T> source, GraphNode<T> target, double weight = 1.0);
        public abstract bool RemoveEdge(GraphNode<T> node, int index);

        public bool AddEdge(T sourceItem, T targetItem, double weight = 1.0)
        {
            var sourceNode = this.FindFirstNode(sourceItem) ?? throw new ArgumentException("Source Item not found", nameof(sourceItem));
            var targetNode = this.FindFirstNode(targetItem) ?? throw new ArgumentException("Target Item not found", nameof(targetItem));
            return AddEdge(sourceNode, targetNode, weight);
        }

        public void RemoveEdge(T source, T target)
        {
            GraphNode<T>? sourceNode = FindFirstNode(source);
            if (sourceNode != null)
            {
                int targetIndex = sourceNode.Neighbors.FindIndex(i => i.Data!.Equals(target));
                if (targetIndex != -1)
                {
                    RemoveEdge(sourceNode, targetIndex);
                }
            }
        }

        #endregion

        #region Algorithms

        /// <summary>
        /// Generic Breadth-First Seach.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="startNodeIndex"></param>
        public void BFS(Action<T> action, GraphNode<T> startNode)
        {
            Algorithms.BFS.Run(this,action, startNode);
        }

        public void BFS(Action<T> action)
        {
            Algorithms.BFS.Run(this, action);
        }


        public void BFS(Action<T> action, T data)
        {
            GraphNode<T>? node = FindFirstNode(data) ?? throw new ArgumentException("Node for data not found", nameof(data));
            Algorithms.BFS.Run(this, action, node);
        }

        /// <summary>
        /// Generic Depth-First Search.
        /// </summary>
        /// <param name="action"></param>
        public void DFS(Action<T> action, GraphNode<T>? startNode = null)
        {
            Algorithms.DFS.Run(this, action, startNode);
        }

        public void DFS(Action<T> action, T data)
        {
            GraphNode<T>? node = FindFirstNode(data) ?? throw new ArgumentException("Node for data not found", nameof(data));
            Algorithms.DFS.Run(this, action, node);
        }


        public List<GraphEdge<T>>? ShortestPath(GraphNode<T> source, GraphNode<T> target, ShortestPathAlgorithm algorithm = ShortestPathAlgorithm.Dijkstra)
        {
            return algorithm switch
            {
                ShortestPathAlgorithm.Dijkstra => ShortestPathDijkstra.Run(this, source, target),
                _ => new List<GraphEdge<T>>(),  // this should never happen!
            };
        }

        public List<GraphEdge<T>>? ShortestPath(T source, T target, ShortestPathAlgorithm algorithm = ShortestPathAlgorithm.Dijkstra)
        {
            GraphNode<T> sourceNode = FindFirstNode(source) ?? throw new ArgumentException("source node not found!", nameof(source));
            GraphNode<T> targetNode = FindFirstNode(target) ?? throw new ArgumentException("target node not found!", nameof(target));
            return ShortestPath(sourceNode, targetNode, algorithm);
        }

        #endregion

        public void Log(Action<string> action)
        {
            action?.Invoke($"Order = {Order}");
            action?.Invoke($"Size = {Size}");
            foreach (GraphNode<T> node in nodes)
            {
                action?.Invoke(node.ToString());
                foreach (GraphNode<T> adj in node.Neighbors)
                {
                    action?.Invoke($"  -> {adj}");
                }
            }
        }

        /*
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        */
    }
}
