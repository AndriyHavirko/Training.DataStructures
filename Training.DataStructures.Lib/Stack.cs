using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    public class Stack<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> top;

        public Stack()
        {
            this.top = null;
            this.Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public LinkedListNode<T> Top
        {
            get { return this.top; }
        }

        public void Clear()
        {
            top = null;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            var current = this.top;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public T Pop()
        {
            if (this.top == null)
                throw new InvalidOperationException();
            var data = this.top.Data;
            if (this.top.Next != null)
                this.top.Next.Previous = null;
            this.top = this.top.Next;
            this.Count--;
            return data;
        }

        public void Push(T item)
        {
            var newItem = new LinkedListNode<T>(data: item);
            if (this.top != null)
            {
                top.Previous = newItem;
                newItem.Next = top;
            }
            top = newItem;
            this.Count++;
        }

        public void Push(IEnumerable<T> items)
        {
            var syncObj = new Object();
            Parallel.ForEach(items, (item) =>
            {
                lock (syncObj)
                {
                    this.Push(item);
                }
            });
        }
    }
}
