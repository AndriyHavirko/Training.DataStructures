using System;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class LinkedListNodeTests
    {
        [TestCase(0.0)]
        [TestCase(123.45)]
        [TestCase(-67.89)]
        public void ToString(double item)
        {
            var node = new LinkedListNode<double>(data: item);
            Assert.AreEqual(item.ToString(), node.ToString());
        }

        [TestCase(0,1)]
        [TestCase(-1, 1)]
        [TestCase(Int32.MinValue, Int32.MaxValue)]
        public void CompareTo_Bigger(int a, int b)
        {
            var nodeA = new LinkedListNode<int>(data: a);
            var nodeB = new LinkedListNode<int>(data: b);
            Assert.IsTrue(nodeA.CompareTo(nodeB) < 0);
        }

        [TestCase(0, -1)]
        [TestCase(1, -1)]
        [TestCase(Int32.MaxValue, Int32.MinValue)]
        public void CompareTo_Smaller(int a, int b)
        {
            var nodeA = new LinkedListNode<int>(data: a);
            var nodeB = new LinkedListNode<int>(data: b);
            Assert.IsTrue(nodeA.CompareTo(nodeB) > 0);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(Int32.MaxValue, Int32.MaxValue)]
        [TestCase(Int32.MinValue, Int32.MinValue)]
        public void CompareTo_Equal(int a, int b)
        {
            var nodeA = new LinkedListNode<int>(data: a);
            var nodeB = new LinkedListNode<int>(data: b);
            Assert.IsTrue(nodeA.CompareTo(nodeB) == 0);
        }
    }
}
