using Pyther.Collections.Generic;
using Pyther.Collections.Generic.Tree;
namespace Tests;

[TestClass]
public class BinaryTree
{
    [TestMethod]
    public void PreOrder()
    {
        var preOrder = new Queue<int>(new int[] { 1, 2, 4, 5, 3, 6, 7 });

        var binTree = new BinaryTree<int>();
        binTree.Root = new BinaryTreeNode<int>(1);
        binTree.Root.
            Add(2).
                Add(4).Up().
                Add(5).Up().
                Up().
            Add(3).
                Add(6).Up().
                Add(7);

        binTree.Traverse(BinaryTreeTraversal.PreOrder, (node, _) => {
            var a = node.Data;
            var b = preOrder.Dequeue();
            Assert.AreEqual(a, b);
        });
    }

    [TestMethod]
    public void InOrder()
    {
        var preOrder = new Queue<int>(new int[] { 4, 2, 5, 1, 6, 3, 7 });

        var binTree = new BinaryTree<int>();
        binTree.Root = new BinaryTreeNode<int>(1);
        binTree.Root.
            Add(2).
                Add(4).Up().
                Add(5).Up().
                Up().
            Add(3).
                Add(6).Up().
                Add(7);

        binTree.Traverse(BinaryTreeTraversal.InOrder, (node, _) => {
            var a = node.Data;
            var b = preOrder.Dequeue();
            Assert.AreEqual(a, b);
        });
    }

    [TestMethod]
    public void PostOrder()
    {
        var preOrder = new Queue<int>(new int[] { 4, 5, 2, 6, 7, 3, 1 });

        var binTree = new BinaryTree<int>();
        binTree.Root = new BinaryTreeNode<int>(1);
        binTree.Root.
            Add(2).
                Add(4).Up().
                Add(5).Up().
                Up().
            Add(3).
                Add(6).Up().
                Add(7);

        binTree.Traverse(BinaryTreeTraversal.PostOrder, (node, _) => {
            var a = node.Data;
            var b = preOrder.Dequeue();
            Assert.AreEqual(a, b);
        });
    }

    [TestMethod]
    public void LevelOrder()
    {
        var preOrder = new Queue<int>(new int[] { 1, 2, 3, 4, 5, 6, 7 });

        var binTree = new BinaryTree<int>();
        binTree.Root = new BinaryTreeNode<int>(1);
        binTree.Root.
            Add(2).
                Add(4).Up().
                Add(5).Up().
                Up().
            Add(3).
                Add(6).Up().
                Add(7);

        binTree.Traverse(BinaryTreeTraversal.LevelOrder, (node, _) => {
            var a = node.Data;
            var b = preOrder.Dequeue();
            Assert.AreEqual(a, b);
        });
    }
}
