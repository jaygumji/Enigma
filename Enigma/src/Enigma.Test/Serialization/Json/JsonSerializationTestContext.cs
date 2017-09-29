using System;
using System.Collections.Generic;
using System.IO;
using Enigma.Binary;
using Enigma.Serialization;
using Enigma.Serialization.Json;
using Enigma.Testing.Fakes.Entities;
using Xunit;

namespace Enigma.Test.Serialization.Json
{
    public class JsonSerializationTestContext : SerializationTestContext
    {
        private readonly JsonEncoding _encoding;
        private readonly IFieldNameResolver _fieldNameResolver;

        public JsonSerializationTestContext()
        {
            _encoding = JsonEncoding.UTF16LE;
            _fieldNameResolver = new CamelCaseFieldNameResolver();
        }

        public void AssertWriteVisitorCall(string expected, Action<JsonWriteVisitor> action)
        {
            using (var memStream = new MemoryStream()) {
                var buffer = new BinaryWriteBuffer(1024, memStream);
                var writeVisitor = new JsonWriteVisitor(_encoding, _fieldNameResolver, buffer, new Stack<bool>(new [] {true}));
                action(writeVisitor);
                buffer.Flush();
                var actual = _encoding.BaseEncoding.GetString(memStream.ToArray());
                Assert.Equal(expected, actual);
            }
        }

        public void AssertSerialize<T>(string expected, T graph)
        {
            var bytes = Serialize(graph);
            var jsonString = _encoding.BaseEncoding.GetString(bytes);
            Assert.Equal(expected, jsonString);
        }

        protected override ITypedSerializer<T> CreateSerializer<T>()
        {
            return new JsonSerializer<T> {
                Encoding = _encoding,
                FieldNameResolver = _fieldNameResolver
            };
        }
    }
}