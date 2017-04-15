using System;
using Enigma.Caching;

namespace Enigma.Reflection
{
    public class CachedTypeProvider : ITypeProvider
    {

        private readonly MemoryCache<Type, ExtendedType> _innerCache;

        public CachedTypeProvider()
        {
            _innerCache = new MemoryCache<Type, ExtendedType>(CachePolicy.Infinite);
        }

        public ExtendedType Extend(Type type)
        {
            return _innerCache.TrySet(type, t => new ExtendedType(t));
        }

    }
}
