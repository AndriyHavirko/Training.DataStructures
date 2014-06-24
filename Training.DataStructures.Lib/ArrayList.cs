using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    class ArrayList<T>: IList<T> where T: IComparable<T>, IEquatable<T>
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
