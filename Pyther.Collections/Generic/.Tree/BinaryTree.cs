namespace Pyther.Collections.Generic.Tree;

public class BinaryTree<T>
{   
    public BinaryTreeNode<T>? Root { get; set; }

    public BinaryTree(BinaryTreeNode<T>? root = null) {
        Root = root;
    }

    public void Traverse(BinaryTreeTraversal traversalMode, BinaryTreeNode<T>.OnTraverse onTraverse)
    {
        Root?.Traverse(traversalMode, onTraverse);
    }

}