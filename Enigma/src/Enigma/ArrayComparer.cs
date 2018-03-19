using System.Collections.Generic;

namespace Enigma
{
    public sealed class ArrayComparer<T> : IComparer<T[]>
    {
        public static readonly ArrayComparer<T> Default = new ArrayComparer<T>();

        private readonly ListComparer<T[], T> _listComparer;

        public ArrayComparer() : this(Comparer<T>.Default)
        {
        }

        public ArrayComparer(IComparer<T> elementComparer)
        {
            _listComparer = new ListComparer<T[], T>(elementComparer);
        }

        public int Compare(T[] first, T[] second)
        {
            return _listComparer.Compare(first, second);
        }
    }
}