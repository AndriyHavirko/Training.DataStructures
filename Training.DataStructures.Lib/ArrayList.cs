using System;
using System.Collections.Generic;

namespace Training.DataStructures
{
    /// <summary>
    /// Represents a generic ArrayList
    /// </summary>
    public class ArrayList<T>: IList<T>, ICollection<T>, IEnumerable<T> where T: IComparable<T>, IEquatable<T>
    {
        private T[] data;
        private int size;

        private static readonly int DefaultCapacity = 4;

        private readonly Object syncRoot = new Object();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Training.DataStructures.Lib.ArrayList`1"/> class.
        /// </summary>
        public ArrayList()
        {
            data = new T[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Training.DataStructures.Lib.ArrayList`1"/> class.
        /// </summary>
        /// <param name="capacity">Capacity of new instance.</param>
        public ArrayList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            data = new T[capacity];
        }

        /// <summary>
        /// Gets amount of elements in the current collection.
        /// </summary>
        public int Count 
        {
            get { return size; }
        }

        /// <summary>
        /// Gets or sets the capacity of the current collection.
        /// </summary>
        /// <value>The capacity.</value>
        public int Capacity
        {
            get { return data.Length; }
            set
            {
                lock (SyncRoot)
                {
                    if (value != data.Length)
                    {
                        if (value > 0)
                        {
                            var newData = new T[value];
                            if (size > 0)
                                Array.Copy(data, 0, newData, 0, size);
                            data = newData;
                        }
                        else
                        {
                            data = new T[DefaultCapacity];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the collection
        /// </summary>
        public object SyncRoot
        {
            get { return syncRoot; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Training.DataStructures.Lib.ArrayList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException();
                return data[index];
            }
            set
            {
                lock (SyncRoot)
                {
                    if (index < 0 || index >= size)
                        throw new ArgumentOutOfRangeException();
                    data[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets index of the item in the current <see cref="T:Training.DataStructures.Lib.ArrayList`1"/> collection.
        /// </summary>
        /// <returns>Index of the item.</returns>
        /// <param name="item">Item.</param>
        public int IndexOf(T item)
        {
            return Array.IndexOf(data, item);
        }

        /// <summary>
        /// Adds the specified item to the current collection.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        public void Add(T item)
        {
            lock (SyncRoot)
            {
                if (size == data.Length)
                    CheckCapacity(size + 1);
                data[size] = item;
                size++;
            }
        }

        /// <summary>
        /// Clear this collection.
        /// </summary>
        public void Clear()
        {
            lock (SyncRoot)
            {
                if (size > 0)
                {
                    Array.Clear(data, 0, size);
                    size = 0;
                }
            }
        }

        /// <summary>
        /// Determines whether the current collection contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the current collection.</param>
        /// <returns><c>true</c> if the the object was found; otherwise <c>false</c>.</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Copies elements of the current instance to the specified array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Destination array index where to start copying.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(data, 0, array, arrayIndex, size);
        }

        /// <summary>
        /// Insert the specified item item in the specified index.
        /// </summary>
        /// <param name="index">Index where to insert new item.</param>
        /// <param name="item">Item to be inserted.</param>
        public void Insert(int index, T item)
        {
            lock (SyncRoot)
            {
                if (index < 0 || index > size)
                    throw new ArgumentOutOfRangeException();
                if (size == data.Length)
                    CheckCapacity(size + 1);
                Array.Copy(data, index, data, index + 1, size - index);
                data[index] = item;
                size++;
            }
        }

        /// <summary>
        /// Removes an item at index.
        /// </summary>
        /// <param name="index">Index.</param>
        public void RemoveAt(int index)
        {
            lock (SyncRoot)
            {
                if (index < 0 || index > size)
                    throw new ArgumentOutOfRangeException();
                size--;

                Array.Copy(data, index + 1, data, index, size - index);
                data[size] = default(T);
            }
        }

        /// <summary>
        /// Removes the first occurrence of an item from the current collection.
        /// </summary>
        /// <param name="item">The item to remove from the current collection.</param>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            lock (SyncRoot)
            {
                if (index >= 0)
                {
                    RemoveAt(index);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets generic enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            lock (this)
            {
                for (int i = 0; i < size; i++)
                    yield return data[i];
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <summary>
        /// Sort current collection using Merge Sorting algorithm.
        /// </summary>
        public void MergeSort()
        {
            lock (SyncRoot)
            {
                MergeSort(data, 0, size - 1);
            }
        }

        /// <summary>
        /// Sort current collection with Merge Sorting algorithm using specified comparer.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        public void MergeSort(IComparer<T> comparer)
        {
            lock (SyncRoot)
            {
                MergeSort(data, 0, size - 1, comparer);
            }
        }

        private void MergeSort(T[] array, int start, int end)
        {
            if (start < end)
            {
                int mid = (start + end)/2;
                MergeSort(array, start, mid);
                MergeSort(array, mid + 1, end);
                Merge(array, start, mid, end);
            }
        }

        private void MergeSort(T[] array, int start, int end, IComparer<T> comparer)
        {
            if (start < end)
            {
                int mid = (start + end) / 2;
                MergeSort(array, start, mid, comparer);
                MergeSort(array, mid + 1, end, comparer);
                Merge(array, start, mid, end, comparer);
            }
        }

        private void Merge(T[] array, int start, int mid, int end)
        {
            var mergedArray = new T[end - start + 1];
            int leftCursor = start;
            int rightCursor = mid + 1;
            int mergedArrayCursor = 0;

            while (leftCursor <= mid && rightCursor <= end)
            {
                if (array[leftCursor].CompareTo(array[rightCursor]) < 0)
                {
                    mergedArray[mergedArrayCursor++] = array[leftCursor++];
                }
                else
                {
                    mergedArray[mergedArrayCursor++] = array[rightCursor++];
                }
            }
            while (leftCursor <= mid)
            {
                mergedArray[mergedArrayCursor++] = array[leftCursor++];
            }
            while (rightCursor <= end)
            {
                mergedArray[mergedArrayCursor++] = array[rightCursor++];
            }
            
            mergedArrayCursor = 0;
            int i = start;
            while (i <= end && mergedArrayCursor < mergedArray.Length)
            {
                array[i++] = mergedArray[mergedArrayCursor++];
            }

        }

        private void Merge(T[] array, int start, int mid, int end, IComparer<T> comparer)
        {
            var mergedArray = new T[end - start + 1];
            int leftCursor = start;
            int rightCursor = mid + 1;
            int mergedArrayCursor = 0;

            while (leftCursor < mid && rightCursor < end)
            {
                if (comparer.Compare(array[leftCursor], array[rightCursor]) < 0)
                {
                    mergedArray[mergedArrayCursor++] = array[leftCursor++];
                }
                else
                {
                    mergedArray[mergedArrayCursor++] = array[rightCursor++];
                }
            }
            while (leftCursor <= mid)
            {
                mergedArray[mergedArrayCursor++] = array[leftCursor++];
            }
            while (rightCursor <= end)
            {
                mergedArray[mergedArrayCursor++] = array[rightCursor++];
            }

            mergedArrayCursor = 0;
            int i = start;
            while (i < end && mergedArrayCursor < mergedArray.Length)
            {
                array[i++] = mergedArray[mergedArrayCursor++];
            }
        }

        private void CheckCapacity(int limit)
        {
            if (data.Length < limit)
            {
                int newCapacity = data.Length == 0 ? DefaultCapacity : data.Length * 2;
                if (newCapacity < limit)
                    newCapacity = limit;
                Capacity = newCapacity;
            }
        }
    }
}
