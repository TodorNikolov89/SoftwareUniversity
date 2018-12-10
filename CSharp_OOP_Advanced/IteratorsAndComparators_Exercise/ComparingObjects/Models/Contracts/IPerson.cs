using System;
using System.Collections.Generic;
using System.Text;

namespace ComparingObjects.Models.Contracts
{
    public interface IPerson
    {
        string Name { get; }

        int Age { get; }

        string Town { get; }
    }
}
