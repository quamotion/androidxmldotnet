using System;

namespace AndroidXmlDemo.Commands
{
    public class ArgumentEventArgs<T> : EventArgs
    {
        public T Argument { get; set; }
    }
}