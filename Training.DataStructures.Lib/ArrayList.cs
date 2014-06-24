using System;
using System.Collections.Generic;

namespace Training.DataStructures.Lib
{
    class ArrayList<T>: IList<T> where T: IComparable<T>, IEquatable<T>
    {
        private T[] data;
        private int size;

        private const int DefaultCapacity = 4;

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

        public int Count {
            get { return size; }
        }

        public int Capacity {
            get
            {
                return data.Length;
            }
            set
            {
                if (value != data.Length)
                {
                    if (value > 0)
                    {
                        T[] newData = new T[value];
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
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
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
