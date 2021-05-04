using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    public  interface ICard
    {
        int Suite { get; }
        int Value { get; }
    }
}
