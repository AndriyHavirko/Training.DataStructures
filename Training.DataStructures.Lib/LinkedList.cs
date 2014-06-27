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

        public int Count { get; private set; }

        public LinkedListNode<T> First
        {
            get { return first; }
        }

        public LinkedListNode<T> Last
        {
            get { return last; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Add an element to list
        /// </summary>
        /// <param name="item"></param>
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
        /// Add a collection of elements to list
        /// </summary>
        /// <param name="items"></param>
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
        /// Clear the list
        /// </summary>
        public void Clear()
        {
            first = null;
            last = null;
            Count = 0;
        }

        /// <summary>
        /// Check if list contains specific item
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Returns true if item was found otherwise returns false</returns>
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

        public void CopyTo(T[] array, int arrayIndex)
        {
            var current = first;
            for (int i = arrayIndex; i < Count + arrayIndex; i++, current = current.Next)
            {
                array[i] = current.Data;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = first;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Get a LinkeListNode object containing the item
        /// </summary>
        /// <param name="item">Item to be found</param>
        /// <returns>Returns LinkedListNode object containing the item, or null if item was not found</returns>
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
        /// Remove the first occurance of the item in list
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>Returns true if item was removed otherwise returns false</returns>
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
        /// Get min item in the list
        /// </summary>
        /// <returns>Min item</returns>
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
        /// Get Max item in the list
        /// </summary>
        /// <returns>Max item</returns>
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
        /// Sort list
        /// </summary>
        public void Sort()
        {
            lock (this)
            {
                BubbleSort();
            }
        }

        /// <summary>
        /// Sort list asynchronously
        /// </summary>
        /// <returns>Task</returns>
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
