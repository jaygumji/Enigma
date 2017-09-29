using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Enigma.Binary.Algorithm;
using Enigma.Scheduling;

namespace Enigma.Binary
{
    public class BinaryBufferPool : IBinaryBufferPool, IDisposable
    {
        private static readonly Lazy<IBinaryBufferPool> LazySingleton
            = new Lazy<IBinaryBufferPool>(() => new BinaryBufferPool(1024, TimeSpan.FromMinutes(20)));
        public static IBinaryBufferPool Instance => LazySingleton.Value;

        private readonly int _size;
        private readonly TimeSpan _slidingExpiration;
        private readonly SortedList<int, BufferLevel> _bufferLevels;
        private readonly object _lockObj = new object();
        private readonly DateTimeQueue<BufferLevel> _slidingQueue;
        private Timer _timer;

        /// <summary>
        /// Creates a new instance of <see cref="BinaryBufferPool"/>
        /// with size of new buffers set to 1024.
        /// </summary>
        public BinaryBufferPool()
            : this(1024, TimeSpan.Zero)
        {
        }

        public BinaryBufferPool(int size)
            : this(size, TimeSpan.Zero)
        {
        }

        public BinaryBufferPool(int size, TimeSpan slidingExpiration)
        {
            _size = size;
            _slidingExpiration = slidingExpiration;
            _bufferLevels = new SortedList<int, BufferLevel>();
            _slidingQueue = new DateTimeQueue<BufferLevel>();
        }

        public BinaryWriteBuffer AcquireWriteBuffer(Stream stream)
        {
            return new BinaryWriteBuffer(this, AcquireBuffer(_size), stream);
        }

        public BinaryReadBuffer AcquireReadBuffer(Stream stream)
        {
            return new BinaryReadBuffer(this, AcquireBuffer(_size), stream);
        }

        public byte[] AcquireBuffer(int minSize)
        {
            var rest = minSize % _size;
            if (rest > 0) {
                // Makes all sized divisable by _size
                // So that we have less buffer levels
                minSize += _size - rest;
            }

            lock (_lockObj) {
                if (_bufferLevels.Count == 0) {
                    _bufferLevels.Add(_size, new BufferLevel(_size));
                }

                if (_size == minSize) {
                    return _bufferLevels.Values[0].PopOrCreate();
                }

                if (_bufferLevels.TryGetValue(minSize, out var level)) {
                    return level.PopOrCreate();
                }

                var closestSizeIndex = BinarySearch.Search(_bufferLevels.Keys, minSize);

                if (_bufferLevels.Keys[closestSizeIndex] > minSize) {
                    level = _bufferLevels.Values[closestSizeIndex];
                    if (level.TryPop(out var buffer)) {
                        return buffer;
                    }
                }
                if (_bufferLevels.Count > closestSizeIndex + 1) {
                    level = _bufferLevels.Values[closestSizeIndex + 1];
                    if (level.TryPop(out var buffer)) {
                        return buffer;
                    }
                }

                level = new BufferLevel(minSize);
                _bufferLevels.Add(minSize, level);
                return new byte[minSize];
            }
        }

        public void Release(byte[] buffer)
        {
            var size = buffer.Length;
            BufferLevel level;
            lock (_lockObj) {
                if (!_bufferLevels.TryGetValue(size, out level)) {
                    if (size % _size > 0) {
                        throw new InvalidOperationException("An attempt to release an invalid buffer detected.");
                    }
                    level = new BufferLevel(size);
                    _bufferLevels.Add(size, level);
                }
            }
            level.Release(buffer);
            if (_slidingExpiration <= TimeSpan.Zero) return;
            _slidingQueue.Enqueue(DateTime.Now.Add(_slidingExpiration), level);
            SetupTimer();
        }

        private void SetupTimer()
        {
            if (!_slidingQueue.TryPeekNextEntryAt(out var nextTimerDueAt)) {
                return;
            }
            var timeToNext = nextTimerDueAt.Subtract(DateTime.Now);
            if (_timer == null) {
                _timer = new Timer(TimerCallback_DiscardUnused, null, timeToNext, Timeout.InfiniteTimeSpan);
            }
            else {
                _timer.Change(timeToNext, Timeout.InfiniteTimeSpan);
            }
        }

        private void TimerCallback_DiscardUnused(object state)
        {
            while (_slidingQueue.TryDequeue(out var levels)) {
                var minTimeStamp = DateTime.Now.Subtract(_slidingExpiration);
                foreach (var level in levels) {
                    level.RemoveUnused(minTimeStamp);
                }
            }
            SetupTimer();
        }

        public void Dispose()
        {
            if (_timer != null) {
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}