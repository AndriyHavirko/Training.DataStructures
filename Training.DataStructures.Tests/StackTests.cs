using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<String> stackOfStrings;
        private Stack<Int32> stackOfInts;
        
        [SetUp]
        public void SetUp()
        {
            stackOfStrings = new Stack<string>();
            stackOfInts = new Stack<int>();
        }

        [TestCase]
        public void Emty()
        {
            Assert.AreEqual(stackOfInts.Count, 0);
            Assert.AreEqual(stackOfStrings.Count, 0);
            Assert.Throws<InvalidOperationException>(() => stackOfInts.Pop());
            Assert.Throws<InvalidOperationException>(() => stackOfStrings.Pop());
        }

        [TestCase]
        public void Push()
        {
            stackOfInts.Push(2);
            stackOfInts.Push(4);
            stackOfInts.Push(8);
            stackOfInts.Push(16);
            Assert.AreEqual(stackOfInts.Count, 4);
            Assert.IsTrue(stackOfInts.Contains(2));
            Assert.IsTrue(stackOfInts.Contains(4));
            Assert.IsTrue(stackOfInts.Contains(8));
            Assert.IsTrue(stackOfInts.Contains(16));

            stackOfStrings.Push(String.Empty);
            stackOfStrings.Push("");
            stackOfStrings.Push("Test");
            Assert.AreEqual(stackOfStrings.Count, 3);
            Assert.IsTrue(stackOfStrings.Contains(String.Empty));
            Assert.IsTrue(stackOfStrings.Contains(""));
            Assert.IsTrue(stackOfStrings.Contains("Test"));
            
        }
    }
}
