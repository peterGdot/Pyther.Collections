namespace Pyther.Collections.Generic.Tree;

public class BinaryTreeNode<T>
{
    public delegate void OnTraverse(BinaryTreeNode<T> node, int depth);

    #region Properties

    /// <summary>The node content.</summary>
    public T Data { get; set; }

    /// <summary>Optional parent node. Only null for root nodes.</summary>
    public BinaryTreeNode<T>? Parent { get; internal set; }
    public BinaryTreeNode<T>? Left { get; internal set; }
    public BinaryTreeNode<T>? Right { get; internal set; }

    #endregion

    #region Calculated Properties

    /// <summary>Is this node the root node (parent is null)?</summary>
    public bool IsRoot => Parent == null;

    public bool HasParent => Parent != null;

    /// <summary>Is this node a lead node (node without children)?</summary>
    public bool IsLeaf => Degree == 0;

    /// <summary>Total amount of direct children.</summary>
    public int Degree => (Left != null ? 1 : 0) + (Right != null ? 1 : 0);

    /// <summary>Return enumeration of all Children (if any).</summary>
    public IEnumerable<BinaryTreeNode<T>> Children
    {
        get
        {
            if (Left != null)
            {
                yield return Left;
            }
            if (Right != null)
            {
                yield return Right;
            }
        }
    }

    /// <summary>Return enumeration of all siblings (if any).</summary>
    public IEnumerable<BinaryTreeNode<T>> Sibings
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
    public IEnumerable<BinaryTreeNode<T>> Path => ReversePath.Reverse();

    /// <summary>Get the path from this node to the root.</summary>
    public IEnumerable<BinaryTreeNode<T>> ReversePath
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
    // public List<double>? Weights { get; internal set; }

    public BinaryTreeNode(T data)
    {
        Data = data;
    }


    #region Add/Remove

    public BinaryTreeNode<T> Add(T data)
    {
        var node = new BinaryTreeNode<T>(data);
        node.Parent = this;
        if (Left == null)
        {
            Left = node;
        } else if (Right == null)
        {
            Right = node;
        } else
        {
            throw new Exception("BinaryTreeNode.Add: Can't have more than two childrens!");
        }
        return node;
    }

    public BinaryTreeNode<T> AddLeft(T data)
    {
        var node = new BinaryTreeNode<T>(data);
        node.Parent = this;
        if (Left != null) Left.Parent = null;
        Left = node;     
        return node;
    }

    public BinaryTreeNode<T> AddRight(T data)
    {
        var node = new BinaryTreeNode<T>(data);
        node.Parent = this;
        if (Right != null) Right.Parent = null;
        Right = node;
        return node;
    }

    public void Remove(BinaryTreeNode<T> node)
    {        
        if (Left == node)
        {
            node.Parent = null;
            Left = null;
        }
        else if (Right == node)
        {
            node.Parent = null;
            Right = null;
        }
        else
        {
            throw new Exception("BinaryTreeNode.Remove: node is not part of the children!");
        }
    }

    public void Remove(int index)
    {
        if (index == 0)
        {
            if (Left != null) Left.Parent = null;
            Left = null;
        }
        else if (index == 1)
        {
            if (Right != null) Right.Parent = null;
            Right = null;
        }
        else
        {
            throw new ArgumentOutOfRangeException("BinaryTreeNode.Remove: Index out of range!");
        }
    }

    public void Remove(T data)
    {
        if (Left != null && Left.Data != null && Left.Data.Equals(data))
        {
            if (Left != null) Left.Parent = null;
            Left = null;
        }
        else if (Right != null && Right.Data != null && Right.Data.Equals(data))
        {
            if (Right != null) Right.Parent = null;
            Right = null;
        }
        else
        {
            throw new Exception("BinaryTreeNode.Remove: Node with this data not found!");
        }
    }

    #endregion

    #region Traverse

    public void Traverse(BinaryTreeTraversal traversalMode, OnTraverse onTraverse)
    {
        switch (traversalMode)
        {
            case BinaryTreeTraversal.PreOrder:
                PreOrder(onTraverse, 0);
                break;
            case BinaryTreeTraversal.InOrder:
                InOrder(onTraverse, 0);
                break;
            case BinaryTreeTraversal.PostOrder:
                PostOrder(onTraverse, 0);
                break;
            case BinaryTreeTraversal.LevelOrder:
                LevelOrder(onTraverse, 0);
                break;
            default:
                break;
        }
    }

    public void PreOrder(OnTraverse onTraverse, int depth)
    {
        onTraverse(this, depth);
        if (this.Left != null) Left.PreOrder(onTraverse, depth + 1);
        if (this.Right != null) Right.PreOrder(onTraverse, depth + 1);
    }

    public void InOrder(OnTraverse onTraverse, int depth)
    {
        if (this.Left != null) Left.InOrder(onTraverse, depth + 1);
        onTraverse(this, depth);
        if (this.Right != null) Right.InOrder(onTraverse, depth + 1);
    }

    public void PostOrder(OnTraverse onTraverse, int depth)
    {        
        if (this.Left != null) Left.PostOrder(onTraverse, depth + 1);
        if (this.Right != null) Right.PostOrder(onTraverse, depth + 1);
        onTraverse(this, depth);
    }

    public void LevelOrder(OnTraverse onTraverse, int depth)
    {
        var queue = new Queue<BinaryTreeNode<T>>();
        queue.Enqueue(this);

        int level = 0;
        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var node = queue.Dequeue();
                onTraverse(node, level);
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
            }
            level++;
        }
    }

    #endregion

    public BinaryTreeNode<T> Up()
    {
        if (this.Parent == null)
        {
            throw new Exception("BinaryTreeNode.Up: Can't move up, since the node doesn't have a parent node!");
        }
        return Parent;
    }

    public void Log(Action<string> action, string inset = "--")
    {
        this.PreOrder((n, d) => action?.Invoke($"{inset.Repeat((uint)d)}{n.Data?.ToString()}"), 0);
    }

}