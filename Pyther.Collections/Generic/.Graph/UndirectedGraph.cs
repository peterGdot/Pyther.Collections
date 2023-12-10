namespace Pyther.Collections.Generic
{
    public class UndirectedGraph<T> : Graph<T>
    {
        public UndirectedGraph(GraphFlags flags = GraphFlags.None) : base(flags) { IsDirected = false; }

        #region Edges

        public override bool AddEdge(GraphNode<T> source, GraphNode<T> target, double weight = 1.0)
        {
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

            target.Neighbors.Add(source);
            source.InDegree++;
            if (this.IsWeighted)
            {
                target.Weights!.Add(weight);
            }

            this.Size += 1;
            return true;
        }

        public override bool RemoveEdge(GraphNode<T> node, int index)
        {
            if (index >= 0 && index < node.Neighbors.Count)
            {
                GraphNode<T> partner = node.Neighbors[index];
                int indexReverse = partner.Neighbors.IndexOf(node);

                // Remove Link Partner -> Node
                partner.Neighbors.RemoveAt(indexReverse);
                partner.Weights?.RemoveAt(indexReverse);
                node.InDegree--;

                // Remove Link Node -> Partner
                node.Neighbors.RemoveAt(index);
                node.Weights?.RemoveAt(index);
                partner.InDegree--;

                this.Size--;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
