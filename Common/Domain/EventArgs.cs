using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}
