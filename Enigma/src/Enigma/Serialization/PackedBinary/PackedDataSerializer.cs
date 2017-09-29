using System.IO;
using Enigma.Binary;

namespace Enigma.Serialization.PackedBinary
{

    internal static class PackedDataSerializer
    {
        public static readonly GraphTravellerCollection Travellers = new GraphTravellerCollection();
    }

    public class PackedDataSerializer<T> : ITypedSerializer<T>
    {
        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public PackedDataSerializer() : this(new BinaryBufferFactory())
        {
        } 

        public PackedDataSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine(PackedDataSerializer.Travellers);
        }

        void ITypedSerializer.Serialize(Stream stream, object graph)
        {
            Serialize(stream, (T) graph);
        }

        public T Deserialize(Stream stream)
        {
            var visitor = new PackedDataReadVisitor(stream);
            return _engine.Deserialize<T>(visitor);
        }

        public void Serialize(Stream stream, T graph)
        {
            using (var buffer = _bufferPool.AcquireWriteBuffer(stream)) {
                var visitor = new PackedDataWriteVisitor(buffer);
                _engine.Serialize(visitor, graph);
            }
        }

        object ITypedSerializer.Deserialize(Stream stream)
        {
            return Deserialize(stream);
        }
    }
}
