﻿using System.IO;
using Enigma.Binary;
using Enigma.Serialization.Reflection.Emit;
using Enigma.Serialization;

namespace Enigma.Db.Serialization
{
    internal static class ModelDataSerializer
    {
        internal static readonly DynamicTravellerContext Context = new DynamicTravellerContext(new ModelSerializationReflectionInspector());
    }
    public class ModelDataSerializer<T> : ITypedSerializer<T>
    {

        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public ModelDataSerializer() : this(BinaryBufferPool.Instance)
        {
        } 

        public ModelDataSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine(ModelDataSerializer.Context);
        }

        void ITypedSerializer.Serialize(Stream stream, object graph)
        {
            Serialize(stream, (T) graph);
        }

        public T Deserialize(Stream stream)
        {
            var visitor = new ModelDataReadVisitor(stream);
            return _engine.Deserialize<T>(visitor);
        }

        public void Serialize(Stream stream, T graph)
        {
            using (var buffer = _bufferPool.AcquireWriteBuffer(stream)) {
                var visitor = new ModelDataWriteVisitor(buffer);
                _engine.Serialize(visitor, graph);
            }
        }

        object ITypedSerializer.Deserialize(Stream stream)
        {
            return Deserialize(stream);
        }
    }
}
