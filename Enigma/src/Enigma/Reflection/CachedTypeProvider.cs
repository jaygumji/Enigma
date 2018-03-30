﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
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
