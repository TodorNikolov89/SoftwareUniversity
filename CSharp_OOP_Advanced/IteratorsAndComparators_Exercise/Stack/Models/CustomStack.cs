using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Stack.Models
{
    public class CustomStack<T> : IEnumerable<T>
    {
        private List<T> numbers;

        public CustomStack()
        {
            this.numbers = new List<T>();
        }

        public void Push(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                this.numbers.Add(list[i]);
            }
        }

        public T Pop()
        {
            if (this.numbers.Count == 0)
            {
                throw new ArgumentException("No elements");
            }

            T item = this.numbers[numbers.Count - 1];

            this.numbers.RemoveAt(this.numbers.Count - 1);
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                yield return this.numbers[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
