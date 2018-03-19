using System.Collections.Generic;
using System.Linq;

namespace Enigma
{
    public sealed class ListEqualityComparer<TList, TElement> : IEqualityComparer<TList>
        where TList : IReadOnlyList<TElement>
    {

        public static readonly ListEqualityComparer<TList, TElement> Default
            = new ListEqualityComparer<TList, TElement>();

        private readonly IEqualityComparer<TElement> _elementEqualityComparer;

        public ListEqualityComparer()
            : this(EqualityComparer<TElement>.Default)
        {
        }

        public ListEqualityComparer(IEqualityComparer<TElement> elementEqualityComparer)
        {
            _elementEqualityComparer = elementEqualityComparer;
        }

        public bool Equals(TList first, TList second)
        {
            if (ReferenceEquals(first, second)) {
                return true;
            }
            if (first == null || second == null) {
                return false;
            }
            if (first.Count != second.Count) {
                return false;
            }
            return !first.Where((t, i) => !_elementEqualityComparer.Equals(t, second[i])).Any();
        }

        public int GetHashCode(TList list)
        {
            unchecked {
                if (list == null) {
                    return 0;
                }
                var hash = 17;
                foreach (var element in list) {
                    hash = hash * 31 + _elementEqualityComparer.GetHashCode(element);
                }
                return hash;
            }
        }
    }
}