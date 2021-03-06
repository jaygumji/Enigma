﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.IO;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{

    public class JsonSerializer : ISerializer
    {

        internal static readonly GraphTravellerCollection Travellers = new GraphTravellerCollection(SerializationEngine.Context);

        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public IFieldNameResolver FieldNameResolver { get; set; }
        public JsonEncoding Encoding { get; set; }

        public JsonSerializer() : this(BinaryBufferPool.Instance)
        {
            FieldNameResolver = new CamelCaseFieldNameResolver();
            Encoding = JsonEncoding.UTF16LE;
        }

        public JsonSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine(Travellers);
        }

        public void Serialize(Stream stream, object graph)
        {
            using (var buffer = _bufferPool.AcquireWriteBuffer(stream)) {
                var visitor = new JsonWriteVisitor(Encoding, FieldNameResolver, buffer);
                _engine.Serialize(visitor, graph);
            }
        }

        public object Deserialize(Type type, Stream stream)
        {
            using (var buffer = _bufferPool.AcquireReadBuffer(stream)) {
                var visitor = new JsonReadVisitor(Encoding, FieldNameResolver, buffer);
                return _engine.Deserialize(visitor, type);
            }
        }
    }

    public class JsonSerializer<T> : ITypedSerializer<T>
    {
        private readonly IBinaryBufferPool _bufferPool;
        private readonly SerializationEngine _engine;

        public IFieldNameResolver FieldNameResolver { get; set; }
        public JsonEncoding Encoding { get; set; }

        public JsonSerializer() : this(new BinaryBufferFactory())
        {
        }

        public JsonSerializer(IBinaryBufferPool bufferPool)
        {
            _bufferPool = bufferPool;
            _engine = new SerializationEngine(JsonSerializer.Travellers);
            FieldNameResolver = new CamelCaseFieldNameResolver();
            Encoding = JsonEncoding.UTF16LE;
        }

        void ITypedSerializer.Serialize(Stream stream, object graph)
        {
            Serialize(stream, (T)graph);
        }

        public T Deserialize(string json)
        {
            var bytes = Encoding.BaseEncoding.GetBytes(json);
            using (var stream = new MemoryStream(bytes)) {
                return Deserialize(stream);
            }
        }

        public T Deserialize(Stream stream)
        {
            using (var buffer = _bufferPool.AcquireReadBuffer(stream)) {
                var visitor = new JsonReadVisitor(Encoding, FieldNameResolver, buffer);
                return _engine.Deserialize<T>(visitor);
            }
        }

        public void Serialize(Stream stream, T graph)
        {
            using (var buffer = _bufferPool.AcquireWriteBuffer(stream)) {
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
