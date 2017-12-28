using System;
using Enigma.Binary;
using Enigma.ProofOfConcept;

namespace Enigma.PerformanceTest.ObjectPool
{
    public class BufferPoolPerformanceTest : StressTestConsoleCommand
    {
        private BinaryBufferPool _pool;
        private readonly Random _random = new Random();
        private readonly int[] _sizes = { 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576, 2097152, 4194304, 8388608, 16777216, 33554432, 67108864, 134217728, 268435456, 536870912 };

        protected override string Name => "Bufferpool";

        protected override void OnInitialize()
        {
            _pool = new BinaryBufferPool();
        }

        protected override void OnRun()
        {
            var idx = _random.Next(0, 20);
            var size = _sizes[idx];
            byte[] buffer = null;
            try {
                buffer = _pool.AcquireBuffer(size);
            }
            finally {
                if (buffer != null) {
                    _pool.Release(buffer);
                }
            }
        }

        protected override void OnDestroy()
        {
            _pool?.Dispose();
        }
    }
}
