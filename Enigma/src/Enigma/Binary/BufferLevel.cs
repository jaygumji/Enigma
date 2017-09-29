using System;
using System.Collections.Generic;
using System.Threading;

namespace Enigma.Binary
{
    public class BufferLevel
    {
        private readonly List<Item> _cachedBuffers;
        private readonly SemaphoreSlim _sem = new SemaphoreSlim(1);

        public int Size { get; }

        private class Item
        {
            public DateTime TimeStamp;
            public byte[] Buffer;
        }

        public BufferLevel(int size)
        {
            Size = size;
            _cachedBuffers = new List<Item>();
        }

        public byte[] PopOrCreate()
        {
            _sem.Wait();
            try {
                if (_cachedBuffers.Count == 0) {
                    return new byte[Size];
                }
                var index = _cachedBuffers.Count - 1;
                var item = _cachedBuffers[index];
                _cachedBuffers.RemoveAt(index);
                return item.Buffer;
            }
            finally {
                _sem.Release();
            }
        }

        public bool TryPop(out byte[] buffer)
        {
            _sem.Wait();
            try {
                if (_cachedBuffers.Count == 0) {
                    buffer = null;
                    return false;
                }
                var index = _cachedBuffers.Count - 1;
                var item = _cachedBuffers[index];
                _cachedBuffers.RemoveAt(index);
                buffer = item.Buffer;
                return true;
            }
            finally {
                _sem.Release();
            }
        }

        public void Release(byte[] buffer)
        {
            var item = new Item {
                Buffer = buffer,
                TimeStamp = DateTime.Now
            };

            _sem.Wait();
            try {
                _cachedBuffers.Add(item);
            }
            finally {
                _sem.Release();
            }
        }

        public void RemoveUnused(DateTime minTimeStamp)
        {
            _sem.Wait();
            try {
                if (_cachedBuffers.Count == 0) {
                    return;
                }
                for (var i = _cachedBuffers.Count - 1; i >= 0; i--) {
                    var item = _cachedBuffers[i];
                    if (item.TimeStamp < minTimeStamp) {
                        _cachedBuffers.RemoveAt(i);
                    }
                }
            }
            finally {
                _sem.Release();
            }
        }
    }

}