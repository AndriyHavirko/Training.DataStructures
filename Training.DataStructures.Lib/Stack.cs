using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    /// <summary>
    /// Represents a simple Stack with basic operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T> where T : IComparable<T>, IEquatable<T>
    {
        private LinkedListNode<T> top;

        public int Count { get; private set; }

        public LinkedListNode<T> Top
        {
            get { return top; }
        }

        public void Clear()
        {
            top = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var current = top;
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
            if (top == null)
                throw new InvalidOperationException(message: "The Stack is empty.");
            
            var data = top.Data;
            if (top.Next != null)
                top.Next.Previous = null; // removing link to current top element, so it could be GCed;
            
            top = top.Next;
            Count--;
            return data;
        }

        public void Push(T item)
        {
            var newItem = new LinkedListNode<T>(data: item);
            if (top != null)
            {
                top.Previous = newItem;
                newItem.Next = top;
            }
            top = newItem;
            Count++;
        }

        public void Push(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Push(item);
            }
        }
    }
}
