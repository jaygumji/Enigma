using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enigma.IO;

namespace Enigma.Binary
{
    public class BinaryBuffer : IDisposable
    {
        private readonly IBinaryBufferPool _pool;
        private readonly Stream _stream;
        private readonly List<BinaryBufferReservation> _reservations;

        private int _size;
        private int _position;
        private byte[] _buffer;
        private BinaryBufferReservation _firstReservation;
        private bool _isDisposed;

        public BinaryBuffer(IBinaryBufferPool pool, int size, Stream stream)
        {
            _size = size;
            _stream = stream;
            _pool = pool;
            _position = 0;
            _buffer = new byte[size];
            _reservations = new List<BinaryBufferReservation>();
        }

        public BinaryBuffer(int size, Stream stream) : this(null, size, stream)
        {
            _stream = stream;
        }

        public byte[] Buffer => _buffer;
        private bool HasReservations => _firstReservation != null;

        public void RequestSpace(int length)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            if (_size - _position > length)
                return;

            if (_firstReservation == null && length <= _size) {
                Flush();
                return;
            }

            var newSize = Math.Max(length, _size*2);
            var newBuffer = new byte[newSize];

            System.Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _position);

            _buffer = newBuffer;
            _size = newSize;
        }

        public void Flush()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            if (_position <= 0) return;

            _stream.Write(_buffer, 0, _position);
            _position = 0;
        }

        public void Use(BinaryBufferReservation reservation)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            var value = (UInt32)(_position - reservation.Position);
            var buffer = BinaryInformation.UInt32.Converter.Convert(value);
            Use(reservation, buffer, 0, buffer.Length);
        }

        public void Use(BinaryBufferReservation reservation, byte[] buffer, int offset, int length)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            if (buffer.Length > reservation.Size)
                throw new ArgumentException("The supplied buffer can not exceed the reservation size");

            System.Buffer.BlockCopy(buffer, offset, _buffer, reservation.Position, buffer.Length);

            if (_firstReservation == reservation) {
                _reservations.RemoveAt(0);
                _firstReservation = _reservations.FirstOrDefault();
                return;
            }

            _reservations.Remove(reservation);
        }

        public BinaryBufferReservation Reserve(int size)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            RequestSpace(size);
            if (!HasReservations) {
                // If less than half of the buffer size is left
                if (_size - _position < size/2) {
                    // Preemptive flush to ensure we have space to write
                    // the data the reservation is about
                    Flush();
                }
            }

            var reservation = new BinaryBufferReservation(_position, size);

            for (var i = 0; i < size; i++) {
                _buffer[_position++] = 0;
            }

            _reservations.Add(reservation);
            if (_firstReservation == null)
                _firstReservation = reservation;

            return reservation;
        }

        public void WriteByte(byte value)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            if (_position >= _size)
                throw new ArgumentException("Buffer is out of space.");
            _buffer[_position++] = value;
        }

        public void Write(byte[] buffer, int offset, int length)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            RequestSpace(length);
            System.Buffer.BlockCopy(buffer, offset, _buffer, _position, length);
            _position += length;
        }

        public int Advance(int length)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferPool");

            RequestSpace(length);
            var position = _position;
            _position += length;
            return position;
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            Flush();

            _isDisposed = true;

            if (_pool != null) {
                _pool.Free(this);
            }

            _buffer = null;
            _position = -1;
        }
    }
}
