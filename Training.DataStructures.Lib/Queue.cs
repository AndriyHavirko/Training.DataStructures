using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.DataStructures.Lib
{
    class Queue<T>
    {
        private LinkedListNode<T> first;
        private LinkedListNode<T> last;

        public Queue()
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

        public bool Contains(T item)
        {
            // implementation here
            return false;
        }

        public void Clear()
        {
 
        }

        public T Dequeue()
        {
            // implementation
            return default(T);
        }

        public void Enqueue(T item)
        {
 
        }

        public void Enqueue(IEnumerable<T> items)
        {
 
        }
    }
}
