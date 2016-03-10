using System.IO;

namespace Enigma.Binary
{
    /// <summary>
    /// Interface IBinaryBufferPool
    /// </summary>
    public interface IBinaryBufferPool
    {
        /// <summary>
        /// Acquires the buffer for the requested stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>BinaryBuffer.</returns>
        BinaryBuffer AcquireBuffer(Stream stream);

        /// <summary>
        /// Frees the specified binary buffer.
        /// </summary>
        /// <param name="binaryBuffer">The binary buffer.</param>
        void Free(BinaryBuffer binaryBuffer);
    }
}