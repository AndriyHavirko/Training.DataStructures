using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    /// <summary>
    /// A double linled list
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    public class LinkedList<T>: ICollection<T> where T: IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        /// <summary>
        /// Gets amount of elements in the current list.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the first <see cref="Training.DataStructures.Lib.LinkedListNode"/> of the current list.
        /// </summary>
        public LinkedListNode<T> First
        {
            get { return first; }
        }

        /// <summary>
        /// Gets the last <see cref="Training.DataStructures.Lib.LinkedListNode"/> of the current list.
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

        /// <summary>
        /// Add the specified item to the list.
        /// </summary>
        /// <param name="item">The item to add to the current collection.</param>
        public void Add(T item)
        {
            lock (this)
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
            lock (this)
            {
                foreach (var item in items)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Add the item after the specified <see cref="Training.DataStructures.Lib.LinkedListNode"/>.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="item">The item to add to the current collection.</param>
        public void AddAfter(LinkedListNode<T> node, T item)
        {
            if (node == last)
                Add(item);
            else
            {
                lock (this)
                {
                    var newNode = new LinkedListNode<T>(item, previous: node, next: node.Next);

                    node.Next.Previous = newNode;
                    node.Next = newNode;
                    Count++;
                }
            }

        }

        /// <summary>
        /// Add the item before the specified <see cref="Training.DataStructures.Lib.LinkedListNode"/>.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="item">The item to add to the current collection.</param>
        public void AddBefore(LinkedListNode<T> node, T item)
        {
            var newNode = new LinkedListNode<T>(item);
            lock (this)
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
            first = null;
            last = null;
            Count = 0;
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
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Find the specified item in the list
        /// </summary>
        /// <param name="item">Item to be found</param>
        /// <returns><see cref="Training.DataStructures.Lib.LinkedListNode"/> object containing the item; <c>null</c> - if the item was not found.</returns>
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
            var nodeToRemove = Find(item);
            if (nodeToRemove == null)
                return false;
            //else
            lock (this)
            {
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
            lock (this)
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
                lock (this)
                {
                    BubbleSort();
                }
            });
        }

        /// <summary>
        /// Swap Data of the specified <see cref="Training.DataStructures.Lib.LinkedListNode"/> instances.
        /// </summary>
        private void Swap(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            var temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;

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
