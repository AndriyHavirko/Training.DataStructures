﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures
{
    /// <summary>
    /// A doubly linked list
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    public class LinkedList<T>: ICollection<T>, ICollection where T: IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        private readonly Object syncRoot = new Object();

        /// <summary>
        /// Gets amount of elements in the current list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the first <see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/> of the current list.
        /// </summary>
        public LinkedListNode<T> First
        {
            get { return first; }
        }

        /// <summary>
        /// Gets the last <see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/> of the current list.
        /// </summary>
        public LinkedListNode<T> Last
        {
            get { return last; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the list
        /// </summary>
        public object SyncRoot
        {
            get { return syncRoot; }
        }

        /// <summary>
        /// Add the specified item to the list.
        /// </summary>
        /// <param name="item">The item to add to the current collection.</param>
        public void Add(T item)
        {
            lock (SyncRoot)
            {
                var newNode = new LinkedListNode<T>(item);
                if (first == null)
                {
                    first = newNode;
                }
                else
                {
                    last.Next = newNode;
                    newNode.Previous = last;
                }
                last = newNode;
                Count++;
            }
        }

        /// <summary>
        /// Adds a collection of items to the list.
        /// </summary>
        /// <param name="items">The collection of item to add to the current list.</param>
        public void Add(IEnumerable<T> items)
        {
            lock (SyncRoot)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Add the item after the specified <see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/>.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="item">The item to add to the current collection.</param>
        public void AddAfter(LinkedListNode<T> node, T item)
        {
            lock (SyncRoot)
            {
                if (node == last)
                    Add(item);
                else
                {
                    var newNode = new LinkedListNode<T>(item, previous: node, next: node.Next);

                    node.Next.Previous = newNode;
                    node.Next = newNode;
                    Count++;
                }
            }
        }

        /// <summary>
        /// Add the item before the specified <see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/>.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="item">The item to add to the current collection.</param>
        public void AddBefore(LinkedListNode<T> node, T item)
        {
            var newNode = new LinkedListNode<T>(item);
            lock (SyncRoot)
            {
                if (node == first)
                {
                    newNode.Next = node;
                    node.Previous = newNode;
                    first = newNode;
                }
                else
                {
                    newNode.Next = node;
                    newNode.Previous = node.Previous;

                    node.Previous.Next = newNode;
                    node.Previous = newNode;
                }
                Count++;
            }
        }

        /// <summary>
        /// Clear this list.
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
        /// Determines whether the current list contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the current list.</param>
        /// <returns><c>true</c> if the the object was found; otherwise <c>false</c>.</returns>
        public bool Contains(T item)
        {
            var current = first;
            while(current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Copies elements of the current list to the specified array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Destination array index where to start copying.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            var current = first;
            for (int i = arrayIndex; i < Count + arrayIndex; i++, current = current.Next)
            {
                array[i] = current.Data;
            }
        }

        /// <summary>
        /// Copies elements of the current list to the specified array.
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

        /// <summary>
        /// Find the specified item in the list
        /// </summary>
        /// <param name="item">Item to be found</param>
        /// <returns><see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/> object containing the item; <c>null</c> - if the item was not found.</returns>
        public LinkedListNode<T> Find(T item)
        {
            var current = first;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }

        /// <summary>
        /// Removes the first occurrence of an item from the current list.
        /// </summary>
        /// <param name="item">The item to remove from the current list.</param>
        /// <returns><c>true</c> if the item was removed; otherwise <c>false</c>.</returns>
        public bool Remove(T item)
        {
            lock (SyncRoot)
            {
                var nodeToRemove = Find(item);
                
                if (nodeToRemove == null)
                    return false;

                if (nodeToRemove == first)
                {
                    first = nodeToRemove.Next;
                    if (nodeToRemove.Next != null)
                        nodeToRemove.Next.Previous = null;
                    else
                        last = null;
                }
                else if (nodeToRemove == last)
                {
                    nodeToRemove.Previous.Next = null;
                    last = nodeToRemove.Previous;
                }
                else
                {
                    nodeToRemove.Previous.Next = nodeToRemove.Next;
                    nodeToRemove.Next.Previous = nodeToRemove.Previous;
                }
                Count--;
                return true;
            }
        }

        /// <summary>
        /// Get the minimum element in the list.
        /// </summary>
        /// <returns>Minimum element.</returns>
        public T Min()
        {
            var currentNode = first;
            var min = currentNode.Data;
            while (currentNode != null)
            {
                if (currentNode.Data.CompareTo(min) < 0)
                    min = currentNode.Data;
                currentNode = currentNode.Next;
            }
            return min;
        }

        /// <summary>
        /// Get the maximum element in the list.
        /// </summary>
        /// <returns>Maximum element.</returns>
        public T Max()
        {
            var currentNode = first;
            var max = currentNode.Data;
            while (currentNode != null)
            {
                if (currentNode.Data.CompareTo(max) > 0)
                    max = currentNode.Data;
                currentNode = currentNode.Next;
            }
            return max;
        }

        /// <summary>
        /// Sort the list.
        /// </summary>
        public void Sort()
        {
            lock (SyncRoot)
            {
                BubbleSort();
            }
        }

        /// <summary>
        /// Sort the list asynchronously.
        /// </summary>
        /// <returns>The <see cref="System.Threading.Tasks.Task"/> instance representing sorting task.</returns>
        public async Task SortAsync()
        {
            await Task.Run(() =>
            {
                lock (SyncRoot)
                {
                    BubbleSort();
                }
            });
        }

        /// <summary>
        /// Swap Data of the specified <see cref="T:Training.DataStructures.Lib.LinkedListNode`1"/> instances.
        /// </summary>
        private void Swap(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            lock (SyncRoot)
            {
                var temp = a.Data;
                a.Data = b.Data;
                b.Data = temp;
            }
        }

        private void BubbleSort()
        {
            // if list is empty or contains only 1 item, it's already sorted;
            if (first == null || first.Next == null)
                return;
            for (int i = 0; i < Count; i++)
            {
                for (var nodeI = first;
                    nodeI != null && nodeI.Next != null;
                    nodeI = nodeI.Next)
                {
                    if (nodeI.CompareTo(nodeI.Next) < 0)
                    {
                        Swap(nodeI, nodeI.Next);
                    }
                }
            }
        }
    }
}
