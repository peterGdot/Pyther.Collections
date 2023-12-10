namespace Pyther.Collections.Generic.Tree;

public class TreeNode<T>
{
    #region Properties

    /// <summary>The node content.</summary>
    public T Data { get; set; }

    /// <summary>Optional parent node. Only null for root nodes.</summary>
    public TreeNode<T>? Parent { get; internal set; }

    /// <summary>List of all children.</summary>
    private List<TreeNode<T>> ChildNodes { get; } = new();

    #endregion

    #region Calculated Properties

    /// <summary>Is this node the root node (parent is null)?</summary>
    public bool IsRoot => Parent == null;

    public bool HasParent => Parent != null;

    /// <summary>Is this node a lead node (node without children)?</summary>
    public bool IsLeaf => Degree == 0;

    /// <summary>Total amount of direct children.</summary>
    public int Degree => ChildNodes.Count;

    /// <summary>Return enumeration of all Children (if any).</summary>
    public IEnumerable<TreeNode<T>> Children
    {
        get
        {
            foreach (var node in ChildNodes)
            {
                yield return node;
            }
        }
    }

    /// <summary>Return enumeration of all siblings (if any).</summary>
    public IEnumerable<TreeNode<T>> Sibings
    {
        get
        {
            if (Parent == null)
            {
                yield break;
            }
            foreach (var node in Parent.Children)
            {
                if (node != this)
                {
                    yield return node;
                }
            }
        }
    }

    /// <summary>Get the path from the root to this node.</summary>
    public IEnumerable<TreeNode<T>> Path => ReversePath.Reverse();

    /// <summary>Get the path from this node to the root.</summary>
    public IEnumerable<TreeNode<T>> ReversePath
    {
        get
        {
            var current = this;
            while (current != null)
            {
                yield return this;
                current = current.Parent;
            }
        }
    }

    #endregion

    /// <summary>Optional list of weights of all children.</summary>
    public List<double>? Weights { get; internal set; }


    public TreeNode(T data)
    {
        Data = data;
    }

    #region Add & Remove

    public void Add(TreeNode<T> node)
    {
        if (node.Parent == this) return;
        if (node.HasParent)
        {
            throw new Exception("The node already has a parent node.");
        }
        this.ChildNodes.Add(node);
        return;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= ChildNodes.Count)
        {
            throw new IndexOutOfRangeException("TreeNode.Remove: Index out of range!");
        }
        this.ChildNodes.RemoveAt(index);
    }

    public TreeNode<T> Add(T data)
    {
        TreeNode<T> treeNode = new(data);
        Add(treeNode);
        return treeNode;
    }

    public void Remove(TreeNode<T> node) {
        if (node.Parent != this)
        {
            throw new Exception("the node is not a child of this node.");
        }
        this.Remove(this.IndexOf(node));
    }

    #endregion

    #region IndexOf - Search Children (not recursiv)
    /// <summary>
    /// Returns the zero based index of the given node if found, -1 otherwise.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int IndexOf(TreeNode<T> node)
    {
        return ChildNodes.IndexOf(node);
    }
    public int IndexOf(TreeNode<T> node, int start)
    {
        return ChildNodes.IndexOf(node, start);
    }
    public int IndexOf(TreeNode<T> node, int start, int count)
    {
        return ChildNodes.IndexOf(node, start, count);
    }

    /// <summary>
    /// Returns the zero based index of the first occurence.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public int IndexOf(T data, int start = 0, int count = -1)
    {
        int end = count == -1 ? ChildNodes.Count - 1 : Math.Min(start + count, ChildNodes.Count - 1);
        for (int i = start; i <= end; i++)
        {
            if (ChildNodes[i].Data != null && ChildNodes[i].Data!.Equals(data))
            {
                return i;
            }
        }
        return -1;
    }

    #endregion


    /// <summary>
    /// Calculate the depth of this node in the tree.
    /// </summary>
    /// <returns>The node in the tree with 0 for roots.</returns>
    public int GetDepth()
    {
        int depth = 0;
        TreeNode<T> current = this;
        while (current.Parent != null)
        {
            depth++;
            current = current.Parent;
        }
        return depth;
    }

    /// <summary>
    /// Check if the given node is an anchestor of this node.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>

    public bool IsAnchestor(TreeNode<T> node)
    {
        return this.AnchestorDistance(node) > 0;
    }

    /// <summary>
    /// Return the distance of the given node to this node, if the node is an anchestor of this node, -1 otherwise.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public int AnchestorDistance(TreeNode<T> node)
    {
        int distance = 0;
        TreeNode<T> current = this;
        while (current.Parent != null)
        {
            if (current == node)
            {
                return distance;
            }
            distance++;
            current = current.Parent;
        }
        return -1;
    }

}
