using System.IO;

namespace Enigma.Binary
{
    public class BinaryBufferPoolFactory : IBinaryBufferPool
    {
        BinaryBuffer IBinaryBufferPool.AcquireBuffer(Stream stream)
        {
            return new BinaryBuffer(1024, stream);
        }

        public void Free(BinaryBuffer binaryBuffer)
        {
        }
    }
}