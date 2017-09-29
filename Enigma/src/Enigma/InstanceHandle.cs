using System;

namespace Enigma
{
    public class InstanceHandle<T> : IDisposable
        where T : class
    {
        private readonly InstancePool<T> _pool;
        public T Instance { get; }

        public InstanceHandle(InstancePool<T> pool, T instance)
        {
            _pool = pool;
            Instance = instance;
        }

        public void Dispose()
        {
            _pool.Release(this);
        }
    }
}