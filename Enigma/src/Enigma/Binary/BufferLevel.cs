using System;
using System.Collections.Generic;

namespace Enigma.Binary
{
    public class BufferLevel
    {
        private readonly LinkedList<Item> _cachedBuffers;
        private readonly object _lockObj = new object();
        public int Size { get; }

        private class Item
        {
            public DateTime TimeStamp;
            public byte[] Buffer;
        }

        public BufferLevel(int size)
        {
            Size = size;
            _cachedBuffers = new LinkedList<Item>();
        }

        public byte[] PopOrCreate()
        {
            lock (_lockObj) {
                if (_cachedBuffers.Count == 0) {
                    return new byte[Size];
                }
                var item = _cachedBuffers.Last.Value;
                _cachedBuffers.RemoveLast();
                return item.Buffer;
            }
        }

        public bool TryPop(out byte[] buffer)
        {
            lock (_lockObj) {
                if (_cachedBuffers.Count == 0) {
                    buffer = null;
                    return false;
                }
                var item = _cachedBuffers.Last.Value;
                _cachedBuffers.RemoveLast();
                buffer = item.Buffer;
                return true;
            }
        }

        public void Release(byte[] buffer)
        {
            lock (_lockObj) {
                _cachedBuffers.AddLast(new Item {
                    Buffer = buffer,
                    TimeStamp = DateTime.Now
                });
            }
        }

        public void RemoveUnused(DateTime minTimeStamp)
        {
            lock (_lockObj) {
                if (_cachedBuffers.Count == 0) {
                    return;
                }
                var node = _cachedBuffers.First;
                while (node != null) {
                    if (node.Value.TimeStamp < minTimeStamp) {
                        var nodeToRemove = node;
                        node = node.Next;
                        _cachedBuffers.Remove(nodeToRemove);
                    }
                    else {
                        node = node.Next;
                    }
                }
            }
        }

    }
}