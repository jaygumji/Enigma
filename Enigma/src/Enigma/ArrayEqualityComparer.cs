using System.Collections.Generic;

namespace Enigma
{
    public sealed class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        public static readonly ArrayEqualityComparer<T> Default = new ArrayEqualityComparer<T>();

        private readonly ListEqualityComparer<T[], T> _listEqualityComparer;

        public ArrayEqualityComparer()
            : this(EqualityComparer<T>.Default)
        {
        }

        public ArrayEqualityComparer(IEqualityComparer<T> elementEqualityComparer)
        {
            _listEqualityComparer = new ListEqualityComparer<T[], T>(elementEqualityComparer);
        }

        public bool Equals(T[] first, T[] second)
        {
            return _listEqualityComparer.Equals(first, second);
        }

        public int GetHashCode(T[] array)
        {
            return _listEqualityComparer.GetHashCode(array);
        }
    }
}