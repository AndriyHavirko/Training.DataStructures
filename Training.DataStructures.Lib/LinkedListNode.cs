using System;

namespace Training.DataStructures.Lib
{
    public class LinkedListNode<T>: IComparable<LinkedListNode<T>> where T: IComparable<T>, IEquatable<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Previous { get; set; }

        public LinkedListNode(T data = default(T), LinkedListNode<T> next = null, LinkedListNode<T> previous = null)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = previous;
        }
        
        public int CompareTo(LinkedListNode<T> other)
        {
            return this.Data.CompareTo(other.Data);
        }

    }
}
