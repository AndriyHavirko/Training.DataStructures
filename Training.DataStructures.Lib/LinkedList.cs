using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Clear()
        {
            this.first = null;
            this.last = null;
            this.Count = 0;
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
                if (currentNode.Data < min)
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
                if (currentNode.Data > max)
                    max = currentNode.Data;
                currentNode = currentNode.Next;
            }
            return max;
        }

        public void BubbleSort()
        {
            //TODO: write implementation
        }

        public void MergeSort()
        {
            //TODO: write implemetation
        }

        protected void Swap (LinkedListNode<T> a, LinkedListNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }
    }
}
