using Pyther.Collections.Generic;

namespace Tests;

[TestClass]
public class MaxHeap
{
    [TestMethod]
    public void Properties()
    {
        var heap = new MaxHeap<int>();

        heap.Insert(10);
        heap.Insert(15);
        heap.Insert(17);
        heap.Insert(6);
        heap.Insert(12);
        heap.Insert(7);

        Assert.AreEqual(heap.Count, 6);
        Assert.AreEqual(heap.Capacity, 16);
        Assert.AreEqual(heap.Empty, false);
        Assert.AreEqual(heap.Peek, 17);
    }

    [TestMethod]
    public void Sorting()
    {
        var heap = new MaxHeap<int>();
        heap.Insert(10);
        heap.Insert(15);
        heap.Insert(17);
        heap.Insert(6);
        heap.Insert(12);
        heap.Insert(7);

        var queue = new Queue<int>(new int[] {17, 15, 12, 10, 7, 6});
        while (!heap.Empty)
        {
            var a = heap.Extract();
            var b = queue.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    [TestMethod]
    public void ReplaceUp()
    {
        var heap = new MaxHeap<int>();
        heap.Insert(10);
        heap.Insert(15);
        heap.Insert(17);
        heap.Insert(6);
        heap.Insert(12);
        heap.Insert(7);

        heap.Replace(7, 16);

        var queue = new Queue<int>(new int[] { 17, 16, 15, 12, 10, 6 });
        while (!heap.Empty)
        {
            var a = heap.Extract();
            var b = queue.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    [TestMethod]
    public void ReplaceDown()
    {
        var heap = new MaxHeap<int>();
        heap.Insert(10);
        heap.Insert(15);
        heap.Insert(17);
        heap.Insert(6);
        heap.Insert(12);
        heap.Insert(7);

        heap.Replace(15, 8);

        var queue = new Queue<int>(new int[] { 17, 12, 10, 8, 7, 6 });
        while (!heap.Empty)
        {
            var a = heap.Extract();
            var b = queue.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    [TestMethod]
    public void CustomObject()
    {
        var heap = new MaxHeap<Person>(Comparer<Person>.Create((a, b) => a.Age.CompareTo(b.Age)), new Person("", int.MaxValue));
        var jane = new Person("Jane Doe", 45);
        heap.Insert(new Person("John Doe", 42));
        heap.Insert(jane);
        heap.Insert(new Person("Max Müller", 28));
        heap.Insert(new Person("Maxi Müller", 26));

        jane.Age = 27;
        heap.Replace(jane, jane, 45.CompareTo(jane.Age));

        var result = new Queue<int>(new int[] { 42, 28, 27, 26 });
        while (!heap.Empty)
        {
            var a = heap.Extract().Age;
            var b = result.Dequeue();
            Assert.AreEqual(a, b);
        }
    }

    internal class PersonComparable : IComparable<PersonComparable>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public PersonComparable(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(PersonComparable? other)
        {
            return this.Age.CompareTo(other?.Age ?? int.MaxValue);
        }
    }

    [TestMethod]
    public void CustomComparable()
    {
        var heap = new MaxHeap<PersonComparable>(Comparer<PersonComparable>.Default, new PersonComparable("", int.MaxValue));
        var jane = new PersonComparable("Jane Doe", 45);
        heap.Insert(new PersonComparable("John Doe", 42));
        heap.Insert(jane);
        heap.Insert(new PersonComparable("Max Müller", 28));
        heap.Insert(new PersonComparable("Maxi Müller", 26));

        jane.Age = 27;
        heap.Replace(jane, jane, 45.CompareTo(jane.Age));

        var result = new Queue<int>(new int[] { 42, 28, 27, 26 });
        while (!heap.Empty)
        {
            var a = heap.Extract().Age;
            var b = result.Dequeue();
            Assert.AreEqual(a, b);
        }
    }
}