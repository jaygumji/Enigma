using System;
using Enigma.Caching;

namespace Enigma.Reflection
{
    public class TypeCache
    {

        private readonly MemoryCache<Type, WrappedType> _innerCache;

        public TypeCache()
        {
            _innerCache = new MemoryCache<Type, WrappedType>(CachePolicy.Infinite);
        }

        public WrappedType Extend(Type type)
        {
            return _innerCache.TrySet(type, t => new WrappedType(t));
        }

    }
}
