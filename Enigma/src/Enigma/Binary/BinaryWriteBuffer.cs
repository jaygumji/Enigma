﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Enigma.Binary
{
    public class BinaryWriteBuffer : BinaryBuffer
    {
        private readonly List<BinaryBufferReservation> _reservations;

        private BinaryBufferReservation _firstReservation;

        public BinaryWriteBuffer(IBinaryBufferPool pool, byte[] buffer, Stream stream)
            : base(pool, buffer, stream)
        {
            _reservations = new List<BinaryBufferReservation>();
        }

        public BinaryWriteBuffer(int size, Stream stream) : this(null, new byte[size], stream)
        {
        }

        private bool HasReservations => _firstReservation != null;

        public void RequestSpace(int length)
        {
            Verify();

            if (Size - Position > length)
                return;

            if (_firstReservation == null && length <= Size) {
                Flush();
                return;
            }

            Expand(length, 0, Position);
        }

        public void Flush()
        {
            Verify();

            if (Position <= 0) return;

            Stream.Write(Buffer, 0, Position);
            Position = 0;
        }

        public void Use(BinaryBufferReservation reservation)
        {
            Verify();

            var value = (UInt32)(Position - reservation.Position);
            var buffer = BinaryInformation.UInt32.Converter.Convert(value);
            Use(reservation, buffer, 0, buffer.Length);
        }

        public void Use(BinaryBufferReservation reservation, byte[] buffer, int offset, int length)
        {
            Verify();

            if (buffer.Length > reservation.Size)
                throw new ArgumentException("The supplied buffer can not exceed the reservation size");

            System.Buffer.BlockCopy(buffer, offset, Buffer, reservation.Position, buffer.Length);

            if (_firstReservation == reservation) {
                _reservations.RemoveAt(0);
                _firstReservation = _reservations.FirstOrDefault();
                return;
            }

            _reservations.Remove(reservation);
        }

        public BinaryBufferReservation Reserve(int size)
        {
            Verify();

            RequestSpace(size);
            if (!HasReservations) {
                // If less than half of the buffer size is left
                if (Size - Position < size/2) {
                    // Preemptive flush to ensure we have space to write
                    // the data the reservation is about
                    Flush();
                }
            }

            var reservation = new BinaryBufferReservation(Position, size);

            for (var i = 0; i < size; i++) {
                Buffer[Position++] = 0;
            }

            _reservations.Add(reservation);
            if (_firstReservation == null)
                _firstReservation = reservation;

            return reservation;
        }

        public void WriteByte(byte value)
        {
            Verify();

            if (Position >= Size)
                throw new ArgumentException("Buffer is out of space.");

            Buffer[Position++] = value;
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        public void Write(byte[] buffer, int offset, int length)
        {
            Verify();

            RequestSpace(length);
            System.Buffer.BlockCopy(buffer, offset, Buffer, Position, length);
            Position += length;
        }

        public int Advance(int length)
        {
            Verify();

            RequestSpace(length);
            var position = Position;
            Position += length;
            return position;
        }

        protected override void OnDispose()
        {
            if (Position <= 0) return;
            // Flush unwritten data to stream
            Stream.Write(Buffer, 0, Position);
        }

    }
}
