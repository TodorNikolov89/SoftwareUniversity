using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Froggy.Models
{
    public class Lake : IEnumerable<int>
    {
        private List<int> lakeIndexes;

        public Lake(int[] elements)
        {
            this.LakeIndexes = new List<int>(elements);
        }

        public List<int> LakeIndexes
        {
            get { return lakeIndexes; }
            set { lakeIndexes = value; }
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < this.LakeIndexes.Count; i++)
            {
                if (i % 2 == 0)
                {
                    yield return this.LakeIndexes[i];
                }
            }

            for (int i = this.LakeIndexes.Count - 1; i >= 0; i--)
            {
                if (i % 2 != 0)
                {
                    yield return this.LakeIndexes[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
