using System;
using System.Collections.Generic;

namespace Training.DataStructures
{
    /// <summary>
    /// Represents a simple Queue class with basic operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Queue<T>: ICollection<T>, IEnumerable<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        private readonly Object syncRoot = new Object();

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
        /// Gets an object that can be used to synchronize access to the stack
        /// </summary>
        public object SyncRoot
        {
            get { return syncRoot; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            Enqueue(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
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
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}
