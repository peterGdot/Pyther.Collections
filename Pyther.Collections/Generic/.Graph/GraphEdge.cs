namespace Pyther.Collections.Generic.Graph
{
    public class GraphEdge<T> : IComparable<GraphEdge<T>>
    {
        public GraphNode<T> Source { get; }
        public GraphNode<T> Target { get; }
        public double? Weight { get; }

        public bool IsSelfLoop => Source == Target;

        public GraphEdge(GraphNode<T> source, GraphNode<T> target, double? weight = null)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Edge: {Source.Data} -- {Weight} --> {Target.Data}";
        }

        public int CompareTo(GraphEdge<T>? other)
        {
            if (other == null || this.Weight > other.Weight)
            {
                return 1;
            } else if (this.Weight < other.Weight) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}
