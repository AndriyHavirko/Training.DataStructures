using System;

namespace Training.DataStructures.Lib
{
    /// <summary>
    /// Represents a node of a LinkedList collection
    /// </summary>
    public class LinkedListNode<T>: IComparable<LinkedListNode<T>> where T: IComparable<T>, IEquatable<T>
    {
        /// <summary>
        /// Gets or sets the <see cref="Training.DataStructures.Lib.LinkedListNode"/> content.
        /// </summary>
        /// <value>The data.</value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the next <see cref="Training.DataStructures.Lib.LinkedListNode"/> item.
        /// </summary>
        /// <value>The next item.</value>
        public LinkedListNode<T> Next { get; set; }

        /// <summary>
        /// Gets or sets the previous <see cref="Training.DataStructures.Lib.LinkedListNode"/> item.
        /// </summary>
        /// <value>The previous item.</value>
        public LinkedListNode<T> Previous { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Training.DataStructures.Lib.LinkedListNode"/> class.
        /// </summary>
        /// <param name="data">The <see cref="Training.DataStructures.Lib.LinkedListNode"/> content.</param>
        /// <param name="next">The next item.</param>
        /// <param name="previous">The previous item.</param>
        public LinkedListNode(T data = default(T), LinkedListNode<T> next = null, LinkedListNode<T> previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        /// <summary>
        /// Returns the sort order of the current instance compared to the specified item.
        /// </summary>
        /// <param name="other">Another item.</param>
        public int CompareTo(LinkedListNode<T> other)
        {
            return Data.CompareTo(other.Data);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Training.DataStructures.Lib.LinkedListNode"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="Training.DataStructures.Lib.LinkedListNode"/>.</returns>
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
