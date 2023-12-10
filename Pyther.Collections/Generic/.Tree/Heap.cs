using System.Reflection;

namespace Pyther.Collections.Generic;

public abstract class Heap<T>
{
    #region Attributes

    private int capacity;
    private readonly int startCapacity;
    /// <summary>The heap (full binary tree) represented as an array.</summary>    
    protected T[] data;
    protected IComparer<T> comparer;
    protected T? bestPeekValue;    

    #endregion

    #region Properties

    /// <summary>The current heap size.</summary>
    public int Count { get; protected set; }
    /// <summary>The current heap capacity.</summary>
    public int Capacity => capacity;

    public bool Empty => Count <= 0;

    #endregion

    public Heap(IComparer<T> comparer, int startCapacity = 16)
    {
        this.comparer = comparer;
        this.startCapacity = startCapacity;
        capacity = startCapacity;
        data = new T[startCapacity];        
    }
    public Heap(int startCapacity = 16) : this(Comparer<T>.Default, startCapacity)
    {
    }

    public Heap(IComparer<T> comparer, T bestPeekValue, int startCapacity = 16) : this(comparer, startCapacity)
    {
        this.bestPeekValue = bestPeekValue;
    }


    protected void EnsureCapacity(int requested)
    {
        if (requested > capacity)
        {
            capacity += (capacity >> 1);  // * 1.5
            if (capacity < requested)
            {
                capacity = requested;
            }
            Array.Resize(ref data, capacity);
        }
    }

    public void TrimExcess()
    {
        capacity = Count;
        Array.Resize(ref data, capacity);
    }

    public int ParentIdx(int index) => index <= 0 ? -1 : (index - 1) >> 1; // (index - 1) / 2;
    public int LeftIdx(int index) => (index << 1) + 1;  // 2 * index + 1;
    public int RightIdx(int index) => (index << 1) + 2;  // 2 * index + 2;

    /// <summary>
    /// Get the heap peek in Θ(1).
    /// </summary>
    /// <returns></returns>
    public T? Peek => data[0];

    /// <summary>
    /// Insert an element to the heap in O(log n).
    /// </summary>
    /// <param name="item"></param>
    public abstract void Insert(T item);
    public abstract void Remove(int index);
    public abstract void Heapify(int index);

    public void Clear()
    {
        capacity = startCapacity;
        data = new T[startCapacity];
        Count = 0;
    }

    /// <summary>
    /// Returns the peek of the heap and remove it in Θ(log n).
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public T Extract()
    {
        if (Empty)
        {
            throw new InvalidOperationException("MinHeap.ExtractMin: Heap is Empty!");
        }
        if (Count == 1)
        {
            Count--;
            return data[0];
        }
        T result = data[0];
        data[0] = data[Count - 1];
        Count--;
        Heapify(0);
        return result;
    }

    public abstract void ReplaceByIndex(int index, T newValue);
    public abstract void ReplaceByIndex(int index, T newValue, int compareResult);

    /// <summary>
    /// Find the internal index of a given item.
    /// </summary>
    /// <param name="item">The item to find using CompareTo.</param>
    /// <returns>Returns the internal index oon success, -1 otherwise.</returns>
    public int Find(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            // if (data[i].CompareTo(item) == 0)
            // if (comparer.Compare(data[i], item) == 0)
            if (data[i] != null && data[i]!.Equals(item))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Chick if the given item exists in the heap.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(T item) => Find(item) != -1;

    /// <summary>
    /// Replace the first occurrance of oldItem with newItem.
    /// </summary>
    /// <param name="oldItem">The value to replace.</param>
    /// <param name="newItem">The new value to set.</param>
    /// <returns>Returns true on success or false if the oldItem was not found.</returns>
    public bool Replace(T oldItem, T newItem) {
        int index = Find(oldItem);
        if (index != -1)
        {
            ReplaceByIndex(index, newItem);
            return true;
        } else
        {
            return false;
        }
    }

    public bool Replace(T oldItem, T newItem, int compareResult)
    {
        int index = Find(oldItem);
        if (index != -1)
        {
            ReplaceByIndex(index, newItem, compareResult);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Log(Action<string> action)
    {
        var items = string.Join(", ", data.Where((v, i) => i < Count));
        action?.Invoke($"Count = {Count}{Environment.NewLine}Capacity = {Capacity}{Environment.NewLine}Items = {items}");
    }
}
