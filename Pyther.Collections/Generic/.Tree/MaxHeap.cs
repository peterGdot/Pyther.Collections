namespace Pyther.Collections.Generic;

public class MaxHeap<T> : Heap<T>
{
    public MaxHeap(IComparer<T> comparer, int startCapacity = 16) : base(comparer, startCapacity)
    {
        try
        {
            bestPeekValue = Utils.GetStaticPropertyValue<T>("MaxValue");
        }
        catch (Exception)
        {
            throw new Exception("Can't create MaxHeap. Add a best peek value (highest possible value) to the constructor if the datatype doesn't have a public static 'MaxValue' property.");
        }
    }

    public MaxHeap(int startCapacity = 16) : base(startCapacity)
    {
        try
        {
            bestPeekValue = Utils.GetStaticPropertyValue<T>("MaxValue");
        }
        catch (Exception)
        {
            throw new Exception("Can't create MaxHeap. Add a best peek value (highest possible value) to the constructor if the datatype doesn't have a public static 'MaxValue' property.");
        }
    }

    public MaxHeap(IComparer<T> comparer, T bestPeekValue, int startCapacity = 16) : base(comparer, bestPeekValue, startCapacity)
    {

    }

    public override void Insert(T item)
    {
        EnsureCapacity(Count + 1);
        int index = Count;
        data[index] = item;
        Count++;
        while (index != 0 && comparer.Compare(data[index], data[ParentIdx(index)]) > 0)
        {
            int parentIdx = ParentIdx(index);
            (data[index], data[parentIdx]) = (data[parentIdx], data[index]);
            index = parentIdx;
        }
    }

    public override void Remove(int index)
    {
        Increase(index, bestPeekValue!);
        Extract();
    }

    public override void Heapify(int index)
    {
        int l = LeftIdx(index);
        int r = RightIdx(index);
        int max = index;
        if (l < Count && comparer.Compare(data[l], data[max]) > 0)
        {
            max = l;
        }
        if (r < Count && comparer.Compare(data[r], data[max]) > 0)
        {
            max = r;
        }
        if (max != index)
        {
            (data[index], data[max]) = (data[max], data[index]);
            Heapify(max);
        }
    }

    public override void ReplaceByIndex(int index, T newValue)
    {
        switch (comparer.Compare(data[index], newValue))
        {
            case 0:
                return;
            case < 0:
                Increase(index, newValue);
                break;
            default:
                Decrease(index, newValue);
                break;
        }
    }

    public override void ReplaceByIndex(int index, T newValue, int compareResult)
    {
        switch (compareResult)
        {
            case < 0:
                Increase(index, newValue);
                break;
            case > 0:
                Decrease(index, newValue);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Increase the value at given index in O(log n).
    /// </summary>
    /// <param name="index"></param>
    /// <param name="newValue"></param>
    private void Increase(int index, T newValue)
    {
        data[index] = newValue;
        while (index != 0 && comparer.Compare(data[index], data[ParentIdx(index)]) > 0)
        {
            int parentIdx = ParentIdx(index);
            (data[index], data[parentIdx]) = (data[parentIdx], data[index]);
            index = parentIdx;
        }
    }

    private void Decrease(int index, T newValue)
    {
        data[index] = newValue;
        Heapify(index);
    }

}
