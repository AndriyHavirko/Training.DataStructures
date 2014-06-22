using System;

namespace Training.DataStructures.Lib
{
    public class LinkedListNode<T>: IComparable<LinkedListNode<T>> where T: IComparable<T>
    {
        private T data;
        private LinkedListNode<T> next;

        public LinkedListNode()
        {
            this.data = default(T);
            this.next = null;
        }

        public LinkedListNode(T data, LinkedListNode<T> next)
        {
            this.data = data;
            this.next = next;
        }

        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public LinkedListNode<T> Next
        {
            get { return this.next; }
            set { this.next = value; }
        }

        public int CompareTo(LinkedListNode<T> other)
        {
            return this.Data.CompareTo(other.Data);
        }

    }
}
