using System.IO;

namespace Enigma.Binary
{
    public class BinaryBufferFactory : IBinaryBufferPool
    {
        private readonly int _size;

        public BinaryBufferFactory()
            : this(1024)
        {
        }

        public BinaryBufferFactory(int size)
        {
            _size = size;
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
            return new byte[minSize];
        }

        public void Release(byte[] buffer)
        {
        }
    }
}