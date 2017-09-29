﻿using System;
using System.IO;

namespace Enigma.Binary
{
    public class BinaryBuffer : IDisposable
    {
        private readonly IBinaryBufferPool _pool;
        private bool _isDisposed;

        protected Stream Stream { get; }

        public byte[] Buffer { get; private set; }
        public int Position { get; protected set; }
        protected int Size { get; set; }

        public BinaryBuffer(IBinaryBufferPool pool, byte[] buffer, Stream stream)
        {
            Size = buffer.Length;
            Stream = stream;
            _pool = pool;
            Position = 0;
            Buffer = buffer;
        }

        protected void Expand(int length, int keepPosition, int keepLength)
        {
            Verify();

            var newSize = Math.Max(length, Size * 2);

            var newBuffer = _pool.AcquireBuffer(newSize);
            System.Buffer.BlockCopy(Buffer, keepPosition, newBuffer, 0, keepLength);
            _pool.Release(Buffer);

            Buffer = newBuffer;
            Size = newBuffer.Length;
        }

        protected void Verify()
        {
            if (_isDisposed) {
                throw new ObjectDisposedException("BufferPool");
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;

            OnDispose();

            _pool?.Release(Buffer);

            Buffer = null;
            Position = -1;
            Size = 0;
        }

        protected virtual void OnDispose()
        {
        }

    }
}