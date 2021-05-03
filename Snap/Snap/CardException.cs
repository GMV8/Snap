using System;
using System.Collections.Generic;
using System.Text;

namespace Snap
{
    public class CardException : Exception
    {
        public CardException(string message) : base(message)
        {

        }

        public CardException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
