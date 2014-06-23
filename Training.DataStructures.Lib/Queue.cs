using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    public class Queue<T> where T : IComparable<T>, IEquatable<T>
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

        public void Clear()
        {
            first = null;
            last = null;
            Count = 0;
        }

        public T Dequeue()
        {
            if (First == null)
                throw new InvalidOperationException(message: "The Queue is empty.");

            var data = First.Data;
            if (First.Next != null)
                First.Next.Previous = null; // removing link to current first element, so it could be GCed;

            first = First.Next;
            Count--;
            return data;
        }

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

        public void Enqueue(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }
    }
}
