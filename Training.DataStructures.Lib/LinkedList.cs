using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    public class LinkedList<T> where T: IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        public LinkedList()
        {
            this.first = null;
            this.last = this.first;
            this.Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public LinkedListNode<T> First
        {
            get { return this.first; }
        }

        public LinkedListNode<T> Last
        {
            get { return this.last; }
        }

        public void Add(T item)
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
            this.Count++;
        }

        public void Add(IEnumerable<T> items)
        {
            var syncObj = new Object();
            Parallel.ForEach(items, (item) => {
                lock (syncObj)
                {
                    this.Add(item);
                }
            });
        }

        public void Clear()
        {
            this.first = null;
            this.last = null;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            var current = this.First;
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
            var currentNode = this.first;
            while (currentNode != null)
            {
                if (currentNode.Data.Equals(item))
                {
                    if (currentNode == this.first)
                    {
                        this.first = currentNode.Next;
                        if (currentNode.Next != null)
                            currentNode.Next.Previous = null;
                        else
                            this.last = null;
                    }
                    else if (currentNode == this.last)
                    {
                        currentNode.Previous.Next = null;
                        this.last = currentNode.Previous;
                    }
                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                        currentNode.Next.Previous = currentNode.Previous;
                    }
                    this.Count--;
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public T Min()
        {
            var currentNode = this.First;
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
            var currentNode = this.First;
            var max = currentNode.Data;
            while (currentNode != null)
            {
                if (currentNode.Data.CompareTo(max) > 0)
                    max = currentNode.Data;
                currentNode = currentNode.Next;
            }
            return max;
        }

        public void BubbleSort()
        {
            Task sorting = new Task(this.InnerBubbleSort);
            sorting.Start();
            sorting.Wait();
        }

        public void MergeSort()
        {
            Task sorting = new Task(this.InnerMergeSort);
            sorting.Start();
            sorting.Wait();
        }

        protected void Swap(ref LinkedListNode<T> a, ref LinkedListNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }

        private void InnerBubbleSort()
        {
            // if list is empty or contains only 1 item, it's already sorted;
            if (this.First == null || this.First.Next == null)
                return;
            for (var nodeI = this.First; 
                     nodeI != null; 
                     nodeI = nodeI.Next)
            {
                for (var nodeJ = this.First;
                         nodeJ.Next != null;
                         nodeJ = nodeJ.Next)
                {
                    if (nodeI.CompareTo(nodeJ) < 1)
                        this.Swap(ref nodeI, ref nodeJ);
                }
            }
        }

        private void InnerMergeSort()
        {
            //TODO: add an implemetation
        }
    }
}
