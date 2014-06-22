using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    class Stack<T>
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

        public void Clear()
        {
 
        }

        public bool Contains(T item)
        {
            // TODO: add an implementation
            return false;
        }

        public T Pop()
        {
            // TODO: add an implementation
            return default(T);
        }

        public void Push(T item)
        {

        }

        public void Push(IEnumerable<T> items)
        {
 
        }
    }
}
