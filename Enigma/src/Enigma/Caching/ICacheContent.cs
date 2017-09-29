using System;

namespace Enigma.Caching
{
    public interface ICacheContent<out TKey, out TContent>
    {
        DateTime CreatedAt { get; }
        DateTime LastAccessedAt { get; }
        TKey Key { get; }
        TContent Value { get; }
        DateTime ExpiresAt { get; }
    }
}