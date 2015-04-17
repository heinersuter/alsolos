namespace Alsolos.Commons.Utils
{
    using System;

    public class ValueEventArgs<T> : EventArgs
    {
        public ValueEventArgs()
        {
        }

        public ValueEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}
