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
        BinaryWriteBuffer AcquireWriteBuffer(Stream stream);

        /// <summary>
        /// Acquires the buffer for the requested stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>BinaryBuffer.</returns>
        BinaryReadBuffer AcquireReadBuffer(Stream stream);

        /// <summary>
        /// Get a binary buffer to use.
        /// </summary>
        /// <param name="minSize">The minimum size of the new buffer</param>
        /// <returns>Binary buffer.</returns>
        byte[] AcquireBuffer(int minSize);

        /// <summary>
        /// Releases the specified binary buffer.
        /// </summary>
        /// <param name="buffer">The binary buffer.</param>
        void Release(byte[] buffer);
    }
}