using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Problem1_ListyIterator
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private int internalIndex = 0;
        private List<T> data;

        public ListyIterator()
        {
            this.data = new List<T>();
        }

        public ListyIterator(IEnumerable<T> collectionData)
        {
            this.data = new List<T>(collectionData);
        }

        public bool Move()
        {
            if (this.internalIndex < this.data.Count - 1)
            {
                internalIndex++;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool HasNext() => this.internalIndex < this.data.Count - 1;

        public T Print()
        {
            if (data.Count == 0)
            {
                throw new ArgumentException($"Invalid Operation!");
            }

            return this.data[this.internalIndex];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.data.Count; i++)
            {
                yield return this.data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        private class ListyIteratorEnumerator : IEnumerator<T>
        {
            private List<T> data;
            private int currentIndex;

            public ListyIteratorEnumerator(List<T> data)
            {
                this.Reset();
                this.data = data;
            }

            public T Current => this.data[currentIndex];

            object IEnumerator.Current => this.Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                return ++this.currentIndex < this.data.Count;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }
        }
    }
}
