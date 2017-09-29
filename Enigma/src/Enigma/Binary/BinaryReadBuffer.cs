﻿using System.IO;

namespace Enigma.Binary
{
    public class BinaryReadBuffer : BinaryBuffer
    {

        public int Length { get; private set; }

        public BinaryReadBuffer(IBinaryBufferPool poolHandle, byte[] buffer, Stream stream)
            : base(poolHandle, buffer, stream)
        {
            RefillBuffer();
        }

        public BinaryReadBuffer(int size, Stream stream) : this(null, new byte[size], stream)
        {
        }

        public bool IsEndOfStream => Position == Length && Length < Size;

        public byte ReadByte()
        {
            if (Position == Length) {
                if (Length < Size) {
                    throw new EndOfStreamException();
                }
                RefillBuffer();
                if (Length == 0) {
                    throw new EndOfStreamException();
                }
            }
            return Buffer[Position++];
        }

        private void RefillBuffer()
        {
            var sizeLeft = Size - Position;
            var copyOffset = 0;
            if (Position != Length) {
                System.Buffer.BlockCopy(Buffer, Position, Buffer, 0, sizeLeft);
                copyOffset = sizeLeft;
            }
            Position = 0;

            Length = sizeLeft
                     + Stream.Read(Buffer, copyOffset, Size - copyOffset);
        }

        public void RequestSpace(int length)
        {
            if (Length - Position >= length) {
                return;
            }

            if (length > Size) {
                var sizeLeft = Length - Position;
                var copyOffset = sizeLeft;
                Expand(length, Position, sizeLeft);
                Position = 0;

                Length = sizeLeft
                         + Stream.Read(Buffer, copyOffset, Size - copyOffset);
            }
            else {
                RefillBuffer();
            }
        }

        public byte PeekByte()
        {
            return PeekByte(0);
        }

        public byte PeekByte(int offset)
        {
            var offsetPosition = Position + offset;
            if (offsetPosition >= Length) {
                if (Length < Size) {
                    throw new EndOfStreamException();
                }
                RefillBuffer();
                if (offsetPosition >= Length) {
                    throw new EndOfStreamException();
                }
            }
            return Buffer[offsetPosition];
        }

        public void Advance(int length)
        {
            if (Position == Length) {
                if (Length < Size) {
                    throw new EndOfStreamException();
                }
                RefillBuffer();
                if (Length == 0) {
                    throw new EndOfStreamException();
                }
            }
            Position++;
        }

    }
}