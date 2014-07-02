using System;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class ArrayListTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100000)]
        public void CareateAnEmptyArrayList(int capacity)
        {
            ArrayList<String> list;
            if (capacity == 0)
                list = new ArrayList<String>();
            else
                list = new ArrayList<String>(capacity);
                
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
            Assert.IsTrue(list.Capacity >= capacity);
        }

        [Test]
        public void CreateAnEmptyArrayList_NegativeCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ArrayList<String>(-1));
        }

        [Test]
        public void Contains_CheckIfFalse_OnEmptyArrayList()
        {
            var list = new ArrayList<String>();
            Assert.IsFalse(list.Contains(String.Empty));
            Assert.IsFalse(list.Contains("Test"));
        }

        [Test]
        public void Contains_CheckIfFalse()
        {
            var list = new ArrayList<String>() { "Test 1", "Test 2" };
            Assert.IsFalse(list.Contains(String.Empty));
            Assert.IsFalse(list.Contains("Test 3"));
        }

        [Test]
        public void Contains_CheckIfTrue()
        {
            var list = new ArrayList<String>() { String.Empty, "Test 1", "Test 2" };
            Assert.IsTrue(list.Contains(String.Empty));
            Assert.IsTrue(list.Contains("Test 2"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100000)]
        public void Add_MultibleElementsToArrayList(int count)
        {
            var list = new ArrayList<String>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                list.Add(String.Format("Test {0}", random.Next()));
            }
            Assert.AreEqual(list.Count, count);
            Assert.IsTrue(list.Capacity >= count);
        }

        [Test]
        public void Clear()
        {
            var list = new ArrayList<int>(100);
            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }

            list.Clear();

            Assert.AreEqual(list.Count, 0);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100000000)]
        public void Capacity(int capacity)
        {
            var list = new ArrayList<int>();
            list.Capacity = capacity;
            Assert.AreEqual(list.Capacity, capacity);
        }

        [TestCase(Int32.MinValue)]
        [TestCase(-1)]
        public void Capacity_LessThanZero(int capacity)
        {
            var list = new ArrayList<String>();
            list.Capacity = capacity;
            Assert.IsTrue(list.Capacity >= 0);
        }


    }
}