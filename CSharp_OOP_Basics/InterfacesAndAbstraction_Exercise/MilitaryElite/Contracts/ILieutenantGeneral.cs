using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Contracts
{
    public interface ILieutenantGeneral
    {
        List<IPrivate> Privates { get; }
    }
}
