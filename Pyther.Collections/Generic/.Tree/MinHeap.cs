namespace Pyther.Collections.Generic;

public class MinHeap<T> : Heap<T>
{
    public MinHeap(IComparer<T> comparer, int startCapacity = 16) : base(comparer, startCapacity)
    {
        try
        {
            bestPeekValue = Utils.GetStaticPropertyValue<T>("MinValue");
        } catch (Exception)
        {
            throw new Exception("Can't create MinHeap. Add a best peek value (lowest possible value) to the constructor if the datatype doesn't have a public static 'MinValue' property.");
        }
    }

    public MinHeap(int startCapacity = 16) : base(startCapacity)
    {
        try
        {
            bestPeekValue = Utils.GetStaticPropertyValue<T>("MinValue");
        }
        catch (Exception)
        {
            throw new Exception("Can't create MinHeap. Add a best peek value (lowest possible value) to the constructor if the datatype doesn't have a public static 'MinValue' property.");
        }
    }

    public MinHeap(IComparer<T> comparer, T bestPeekValue, int startCapacity = 16) : base(comparer, bestPeekValue, startCapacity)
    {

    }

    public override void Insert(T item)
    {
        EnsureCapacity(Count + 1);
        int index = Count;
        data[index] = item;
        Count++;
        while (index != 0 && comparer.Compare(data[index], data[ParentIdx(index)]) < 0)
        {
            int parentIdx = ParentIdx(index);
            (data[index], data[parentIdx]) = (data[parentIdx], data[index]);
            index = parentIdx;
        }
    }

    public override void Remove(int index)
    {
        Decrease(index, bestPeekValue!);
        Extract();
    }

    public override void Heapify(int index)
    {
        int l = LeftIdx(index);
        int r = RightIdx(index);
        int min = index;
        if (l < Count && comparer.Compare(data[l], data[min]) < 0)
        {
            min = l;
        }
        if (r < Count && comparer.Compare(data[r], data[min]) < 0)
        {
            min = r;
        }
        if (min != index)
        {
            (data[index], data[min]) = (data[min], data[index]);
            Heapify(min);
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
            case > 0:
                Decrease(index, newValue);
                return;
            case < 0:
                Increase(index, newValue);
                break;
            default:                
                break;
        }
    }

    /// <summary>
    /// Decrease the value at given index in O(log n).
    /// </summary>
    /// <param name="index"></param>
    /// <param name="newValue"></param>
    private void Decrease(int index, T newValue)
    {
        data[index] = newValue;
        while (index != 0 && comparer.Compare(data[index], data[ParentIdx(index)]) < 0)
        {
            int parentIdx = ParentIdx(index);
            (data[index], data[parentIdx]) = (data[parentIdx], data[index]);
            index = parentIdx;
        }
    }

    private void Increase(int index, T newValue)
    {
        data[index] = newValue;
        Heapify(index);
    }

}
