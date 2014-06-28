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

        /// <summary>
        /// Gets an amount of elements in the stack.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the top element of the stack.
        /// </summary>
        public LinkedListNode<T> Top
        {
            get { return top; }
        }

        /// <summary>
        /// Clear this stack.
        /// </summary>
        public void Clear()
        {
            top = null;
            Count = 0;
        }

        /// <summary>
        /// Determines whether the current stack contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the current stack.</param>
        /// <returns><c>true</c> if the the object was found; otherwise <c>false</c>.</returns>
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

        /// <summary>
        /// Removes the top element of the stack and returns its value.
        /// </summary>
        /// <returns>Removed element.</returns>
        public T Pop()
        {
            if (top == null)
                throw new InvalidOperationException(message: "The Stack is empty.");
            
            var data = top.Data;
            if (top.Next != null)
                top.Next.Previous = null; // removing link to current top element
            
            top = top.Next;
            Count--;
            return data;
        }

        /// <summary>
        /// Adds the specified item to the top of the stack.
        /// </summary>
        /// <param name="item">Item to be added to the stack.</param>
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

        /// <summary>
        /// Adds a collection of items to the top of the stack.
        /// </summary>
        /// <param name="items">Items to be added to the stack.</param>
        public void Push(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Push(item);
            }
        }
    }
}
