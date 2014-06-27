using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    public class ArrayList<T>: IList<T> where T: IComparable<T>, IEquatable<T>
    {
        private T[] data;
        private int size;

        private static readonly int DefaultCapacity = 4;

        public ArrayList()
        {
            data = new T[0];
        }

        public ArrayList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            data = new T[capacity];
        }

        public int Count 
        {
            get { return size; }
        }

        public int Capacity
        {
            get { return data.Length; }
            set
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

        public bool IsReadOnly
        {
            get { return false; }
        }

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
                if (index < 0 || index >= size)
                    throw new ArgumentOutOfRangeException();
                data[index] = value;
            }
        }

        public int IndexOf(T item)
        {
            return Array.IndexOf(data, item);
        }

        public void Add(T item)
        {
            if (size == data.Length)
                CheckCapacity(size + 1);
            data[size] = item;
            size++;
        }

        public void Clear()
        {
            if (size > 0)
            {
                Array.Clear(data, 0, size);
                size = 0;
            }
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < size; i++)
            {
                if (data[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(data, 0, array, arrayIndex, size);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > size)
                throw  new ArgumentOutOfRangeException();
            if (size == data.Length)
                CheckCapacity(size + 1);
            Array.Copy(data, index, data, index + 1, size - index);
            data[index] = item;
            size++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException();
            size--;

            Array.Copy(data, index + 1, data, index, size - index);
            data[size] = default(T);
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (this)
            {
                for (int i = 0; i < size; i++)
                    yield return data[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public void MergeSort()
        {
            MergeSort(data, 0, size - 1);
        }

        public void MergeSort(IComparer<T> comparer)
        {
            MergeSort(data, 0, size - 1, comparer);
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
