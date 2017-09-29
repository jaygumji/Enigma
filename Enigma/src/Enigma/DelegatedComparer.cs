using System.Collections;
using System.Collections.Generic;

namespace Enigma
{
    public delegate int CompareHandler<in T>(T x, T y);

    public class DelegatedComparer<T> : IComparer<T>, IComparer
    {
        private readonly CompareHandler<T> _compareCallback;

        public DelegatedComparer(CompareHandler<T> compareCallback)
        {
            _compareCallback = compareCallback;
        }

        public int Compare(T x, T y)
        {
            return _compareCallback.Invoke(x, y);
        }

        int IComparer.Compare(object x, object y)
        {
            if (!(x is T)) {
                return -1;
            }
            if (!(y is T)) {
                return 1;
            }
            return Compare((T) x, (T) y);
        }
    }
}
