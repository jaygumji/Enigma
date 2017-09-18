using System;
using System.IO;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{

    public class JsonSerializer : ISerializer
    {
        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public IFieldNameResolver FieldNameResolver { get; set; }
        public JsonEncoding Encoding { get; set; }

        public JsonSerializer() : this(new BinaryBufferPoolFactory())
        {
            FieldNameResolver = new CamelCaseFieldNameResolver();
            Encoding = JsonEncoding.Unicode;
        }

        public JsonSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine();
        }

        public void Serialize(Stream stream, object graph)
        {
            using (var buffer = _bufferPool.AcquireBuffer(stream)) {
                var visitor = new JsonWriteVisitor(Encoding, FieldNameResolver, buffer);
                _engine.Serialize(visitor, graph);
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            var visitor = new JsonReadVisitor(Encoding, FieldNameResolver, stream);
            return _engine.Deserialize(visitor, type);
        }
    }

    public class JsonSerializer<T> : ITypedSerializer<T>
    {
        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public IFieldNameResolver FieldNameResolver { get; set; }
        public JsonEncoding Encoding { get; set; }


        public JsonSerializer() : this(new BinaryBufferPoolFactory())
        {
        }

        public JsonSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine();
            FieldNameResolver = new CamelCaseFieldNameResolver();
            Encoding = JsonEncoding.Unicode;
        }

        void ITypedSerializer.Serialize(Stream stream, object graph)
        {
            Serialize(stream, (T)graph);
        }

        public T Deserialize(Stream stream)
        {
            var visitor = new JsonReadVisitor(Encoding, FieldNameResolver, stream);
            return _engine.Deserialize<T>(visitor);
        }

        public void Serialize(Stream stream, T graph)
        {
            using (var buffer = _bufferPool.AcquireBuffer(stream)) {
                var visitor = new JsonWriteVisitor(Encoding, FieldNameResolver, buffer);
                _engine.Serialize(visitor, graph);
            }
        }

        object ITypedSerializer.Deserialize(Stream stream)
        {
            return Deserialize(stream);
        }
    }
}
