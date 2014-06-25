using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    public class LinkedList<T> where T: IComparable<T>, IEquatable<T>
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

        public void Add(T item)
        {
            lock (this)
            {
                var newNode = new LinkedListNode<T>(data: item);
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

        public void Add(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            first = null;
            last = null;
            Count = 0;
        }

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

        public bool Remove(T item)
        {
            lock (this)
            {
                var currentNode = first;
                while (currentNode != null)
                {
                    if (currentNode.Data.Equals(item))
                    {
                        if (currentNode == first)
                        {
                            first = currentNode.Next;
                            if (currentNode.Next != null)
                                currentNode.Next.Previous = null;
                            else
                                last = null;
                        }
                        else if (currentNode == last)
                        {
                            currentNode.Previous.Next = null;
                            last = currentNode.Previous;
                        }
                        else
                        {
                            currentNode.Previous.Next = currentNode.Next;
                            currentNode.Next.Previous = currentNode.Previous;
                        }
                        Count--;
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
                return false;
            }
        }

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

        public async Task Sort()
        {
            await Task.Run(() =>
            {
                lock (this)
                {
                    BubbleSort();
                }
            });
        }

        protected void Swap(LinkedListNode<T> a, LinkedListNode<T> b)
        {
            var temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;

        }

        protected void BubbleSort()
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
