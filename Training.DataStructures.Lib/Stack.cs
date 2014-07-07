using System;
using System.Collections;
using System.Collections.Generic;

namespace Training.DataStructures
{
    /// <summary>
    /// Represents a simple Stack with basic operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T>: ICollection, IEnumerable<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> top;

        private readonly Object syncRoot = new Object();

        /// <summary>
        /// Gets an amount of elements in the stack.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the top element of the stack.
        /// </summary>
        public LinkedListNode<T> Top
        {
            get { return top; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the stack
        /// </summary>
        public object SyncRoot
        {
            get { return syncRoot; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Clear this stack.
        /// </summary>
        public void Clear()
        {
            lock (syncRoot)
            {
                top = null;
                Count = 0;
            }
        }

        /// <summary>
        /// Determines whether the current stack contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the current stack.</param>
        /// <returns><c>true</c> if the the object was found; otherwise <c>false</c>.</returns>
        public bool Contains(T item)
        {
            var current = top;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Copies elements of the current stack to the specified array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="index">Destination array index where to start copying.</param>
        public void CopyTo(Array array, int index)
        {
            var current = top;
            for (int i = index; i < Count + index; i++, current = current.Next)
            {
                array.SetValue(current.Data, i); 
            }
        }

        /// <summary>
        /// Gets the generic enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = top;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the top element of the stack and returns its value.
        /// </summary>
        /// <returns>Removed element.</returns>
        public T Pop()
        {
            lock (syncRoot)
            {
                if (top == null)
                    throw new InvalidOperationException(message: Resources.Stack_Pop_EmptyStack);

                var data = top.Data;
                if (top.Next != null)
                    top.Next.Previous = null; // removing link to current top element

                top = top.Next;
                Count--;
                return data;
            }
        }

        /// <summary>
        /// Adds the specified item to the top of the stack.
        /// </summary>
        /// <param name="item">Item to be added to the stack.</param>
        public void Push(T item)
        {
            var newItem = new LinkedListNode<T>(data: item);
            lock (syncRoot)
            {
                if (top != null)
                {
                    top.Previous = newItem;
                    newItem.Next = top;
                }
                top = newItem;
                Count++;
            }
        }

        /// <summary>
        /// Adds a collection of items to the top of the stack.
        /// </summary>
        /// <param name="items">Items to be added to the stack.</param>
        public void Push(IEnumerable<T> items)
        {
            lock (syncRoot)
            {
                foreach (var item in items)
                {
                    Push(item);
                }
            }
        }
    }
}
