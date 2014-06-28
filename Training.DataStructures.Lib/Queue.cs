using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    /// <summary>
    /// Represents a simple Queue class with basic operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        /// <summary>
        /// Gets an amount of elements in the queue.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the first element of the queue.
        /// </summary>
        /// <value>The first element in the queue.</value>
        public LinkedListNode<T> First
        {
            get { return first; }
        }

        /// <summary>
        /// Gets the last element in the queue.
        /// </summary>
        /// <value>The last element in the queue.</value>
        public LinkedListNode<T> Last
        {
            get { return last; }
        }

        /// <summary>
        /// Determines whether the current queue contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the current queue.</param>
        /// <returns><c>true</c> if the the object was found; otherwise <c>false</c>.</returns>
        public bool Contains(T item)
        {
            var current = First;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Clear this queue.
        /// </summary>
        public void Clear()
        {
            first = null;
            last = null;
            Count = 0;
        }

        /// <summary>
        /// Removes the first element of the queue and returns its value.
        /// </summary>
        /// <returns>Removed element.</returns>
        public T Dequeue()
        {
            if (First == null)
                throw new InvalidOperationException(message: "The Queue is empty.");

            var data = First.Data;
            if (First.Next != null)
                First.Next.Previous = null; // removing link to current first element

            first = First.Next;
            Count--;
            return data;
        }

        /// <summary>
        /// Adds the specified item to the end of the queue.
        /// </summary>
        /// <param name="item">Item to be added to the queue.</param>
        public void Enqueue(T item)
        {
            var newItem = new LinkedListNode<T>(data: item);
            if (First == null)
            {
                first = newItem;
            }
            else
            {
                Last.Next = newItem;
                newItem.Previous = Last;
            }
            last = newItem;
            Count++;
        }

        /// <summary>
        /// Adds a collection of items to the end of the queue.
        /// </summary>
        /// <param name="items">Items to be added to the queue.</param>
        public void Enqueue(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }
    }
}
