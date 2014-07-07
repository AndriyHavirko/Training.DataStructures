using System;
using System.Collections;
using System.Collections.Generic;

namespace Training.DataStructures
{
    /// <summary>
    /// Represents a simple Queue class with basic operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>: ICollection, IEnumerable<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        private readonly Object syncRoot = new Object();

        /// <summary>
        /// Gets an amount of elements in the queue.
        /// </summary>
        public int Count { get; private set; }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

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
        /// Gets an object that can be used to synchronize access to the stack
        /// </summary>
        public object SyncRoot
        {
            get { return syncRoot; }
        }

        /// <summary>
        /// Copies elements of the current queue to the specified array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="index">Destination array index where to start copying.</param>
        public void CopyTo(Array array, int index)
        {
            var current = first;
            for (int i = index; i < Count + index; i++, current = current.Next)
            {
                array.SetValue(current.Data, i);
            }
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
            lock (SyncRoot)
            {
                first = null;
                last = null;
                Count = 0;
            }
        }

        /// <summary>
        /// Removes the first element of the queue and returns its value.
        /// </summary>
        /// <returns>Removed element.</returns>
        public T Dequeue()
        {
            lock (SyncRoot)
            {
                if (First == null)
                    throw new InvalidOperationException(message: Resources.Queue_Dequeue_EmptyQueue);

                var data = First.Data;
                if (First.Next != null)
                    First.Next.Previous = null; // removing link to current first element

                first = First.Next;
                Count--;
                return data;
            }
        }

        /// <summary>
        /// Adds the specified item to the end of the queue.
        /// </summary>
        /// <param name="item">Item to be added to the queue.</param>
        public void Enqueue(T item)
        {
            var newItem = new LinkedListNode<T>(data: item);
            lock (SyncRoot)
            {
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
        }

        /// <summary>
        /// Adds a collection of items to the end of the queue.
        /// </summary>
        /// <param name="items">Items to be added to the queue.</param>
        public void Enqueue(IEnumerable<T> items)
        {
            lock (SyncRoot)
            {
                foreach (var item in items)
                {
                    Enqueue(item);
                }
            }
        }

        /// <summary>
        /// Gets the generic enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var current = first;
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
    }
}
