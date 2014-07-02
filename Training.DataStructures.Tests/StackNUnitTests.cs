using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Training.DataStructures.Tests
{
    [TestFixture]
    public class StackNUnitTests
    {
        private Stack<String> stackOfStrings;
        
        private String[] arrayOfStrings;


        [SetUp]
        public void SetUp()
        {
            var random = new Random();
            var size = 100;

            stackOfStrings = new Stack<string>();
            
            arrayOfStrings = new string[size];

            for (int i = 0; i < size; i++)
            {
                arrayOfStrings[i] = String.Format("Test String {0}", random.Next(size));
            }
        }

        [Test]
        public void Emty()
        {
            Assert.AreEqual(stackOfStrings.Count, 0);
            Assert.Throws<InvalidOperationException>(() => stackOfStrings.Pop());
        }

        [Test]
        public void Contains()
        {
            Assert.IsFalse(stackOfStrings.Contains("Test 1"));
            
            stackOfStrings.Push("Test 1");
            Assert.IsTrue(stackOfStrings.Contains("Test 1"));

            stackOfStrings.Push("Test 2");
            stackOfStrings.Push("Test 3");
            Assert.IsTrue(stackOfStrings.Contains("Test 3"));
            Assert.IsTrue(stackOfStrings.Contains("Test 2"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void Push(int size)
        {
            for (int i = 0; i<size; i++)
            {
                stackOfStrings.Push(arrayOfStrings[i]);
                Assert.AreEqual(stackOfStrings.Top.Data, arrayOfStrings[i]);
            }
            Assert.AreEqual(stackOfStrings.Count, size);
        }

        public void PushCollection()
        {
            stackOfStrings.Push(arrayOfStrings);
            Assert.AreEqual(stackOfStrings.Count, arrayOfStrings.Length);
            foreach (var s in arrayOfStrings)
                Assert.IsTrue(stackOfStrings.Contains(s));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        public void Pop(int size)
        {
            for (int i = 0; i < size; i++)
            {
                stackOfStrings.Push(arrayOfStrings[i]);
            }

            for (int i = size - 1; i >= 0; i--)
            {
                Assert.AreEqual(stackOfStrings.Pop(), arrayOfStrings[i]);
            }
        }

        [Test]
        public void Clear()
        {
            stackOfStrings.Push(arrayOfStrings);
            Assert.AreEqual(stackOfStrings.Count, arrayOfStrings.Length);

            stackOfStrings.Clear();
            Assert.AreEqual(stackOfStrings.Count, 0);
            Assert.IsNull(stackOfStrings.Top);
        }
    }
}
