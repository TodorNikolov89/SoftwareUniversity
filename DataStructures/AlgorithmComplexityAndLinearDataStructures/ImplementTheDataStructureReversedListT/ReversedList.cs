using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ImplementTheDataStructureReversedListT
{
    public class ReversedList<T> : IEnumerable<T>
    {
        private T[] data;
        private int index = 2;

        public ReversedList()
        {
            this.data = new T[2];
        }

        public void Add(T item)
        {
            if (this.data.Length == index)
            {
                Array.Resize(ref data, 2 * index);
            }

            this.data[index] = item;
            index++;
        }

        public int Count { get; private set; }

        public int Capacity => this.data.Length;

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return this.data[this.Count - index - 1];
            }

            set
            {
                CheckIndex(index);
                this.data[this.Count - index - 1] = value;
            }
        }

        public T RemoveAt(int index)
        {
            CheckIndex(index);

            var removedItem = this.data[this.Count - 1 - index];

            for (int i = this.Count - index; i < this.Count; i++)
            {
                this.data[i - 1] = this.data[i];
            }

            this.Count--;

            return removedItem;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.data)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
