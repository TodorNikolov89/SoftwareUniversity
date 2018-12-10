namespace Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Database
    {
        private const int dataCapacity = 16;
        private int[] data;
        private int index;

        public Database()
        {
            //TODO test this value;
            this.index = -1;
            this.data = new int[dataCapacity];
        }

        public Database(int[] values) : this()
        {
            if (values.Length > 16)
            {
                throw new InvalidOperationException("Array lenght is too long");
            }

            for (int i = 0; i < values.Length; i++)
            {
                this.data[i] = values[i];
            }

            this.index = values.Length - 1;
        }

        public void Add(int value)
        {
            //TODO test if index value is 15
            if (this.index == dataCapacity - 1)
            {
                throw new InvalidOperationException("Database is full");
            }           

            this.data[++this.index] = value;
        }

        public void Remove()
        {
            //TODO check this case
            if (this.index == -1)
            {
                throw new InvalidOperationException("Database is empty");
            }

            this.data[this.index] = 0;
            this.index--;
        }

        public int[] Fetch()
        {
            return this.data.Take(this.index + 1).ToArray();
        }
    }
}
