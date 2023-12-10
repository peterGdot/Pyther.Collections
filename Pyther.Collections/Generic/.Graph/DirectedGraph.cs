using Pyther.Collections.Generic.Algorithms;

namespace Pyther.Collections.Generic
{
    /// <summary>
    /// Represents i strongly typed graph of priorityQueue (aka. vertices, points) and edges between them which have orientations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DirectedGraph<T> : Graph<T>
    {
        public DirectedGraph(GraphFlags flags = GraphFlags.None) : base(flags) { IsDirected = true; }

        #region Edges

        public override bool AddEdge(GraphNode<T> source, GraphNode<T> target, double weight = 1.0) {
            if (!AllowSelfLoops && source == target)
            {
                return false;
            }
            if (source.Neighbors.Contains(target))
            {
                return false;
            }
            source.Neighbors.Add(target);
            target.InDegree++;
            if (this.IsWeighted)
            {
                source.Weights!.Add(weight);
            }
            this.Size += 1;
            return true;
        }

        public override bool RemoveEdge(GraphNode<T> node, int index)
        {
            if (index >= 0 && index < node.Neighbors.Count)
            {
                node.Neighbors[index].InDegree--;
                node.Neighbors.RemoveAt(index);
                node.Weights?.RemoveAt(index);
                this.Size--;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Algorithms

        public List<T> TopologicalSort(TopologicalSortAlgorithm algorithm = TopologicalSortAlgorithm.Kahn, bool reverse = false)
        {
            return algorithm switch
            {
                TopologicalSortAlgorithm.Kahn => TopologicalSortKahn.Run(this, reverse),
                TopologicalSortAlgorithm.DepthFirstSearch => TopologicalSortDFS.Run(this, reverse),
                _ => new List<T>(),  // this should never happen!
            };
        }

        public bool IsCyclic(IsCyclicAlgorithm algorithm = IsCyclicAlgorithm.DepthFirstSearch)
        {
            return algorithm switch
            {
                IsCyclicAlgorithm.DepthFirstSearch => IsCyclicDFS.Run(this),
                IsCyclicAlgorithm.Kahn => IsCyclicKahn.Run(this),
                _ => false
            };
        }

        #endregion

        /// <summary>
        /// Create a transposed copy of this path, where all edge directions are inverted.
        /// The original graph keeps untouched.
        /// </summary>
        /// <returns>The new created graph.</returns>
        public DirectedGraph<T> Transpose()
        {
            DirectedGraph<T> result = new(this.Flags);
            for (int i = 0; i < this.Order; i++)
            {
                // var clone = new GraphNode<T>(this[i].Data!);
                result.Add(this[i]!.Data);
            }

            foreach (var node in nodes)
            {
                for (int i = 0; i < node.Neighbors.Count; i++)
                {
                    var adj = node.Neighbors[i];
                    result.AddEdge(result[adj.Index]!, result[node.Index]!, node.GetWeight(i));
                }
            }
            return result;
        }

    }
}
