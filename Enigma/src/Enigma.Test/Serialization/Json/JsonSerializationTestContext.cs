using System;
using System.IO;
using Enigma.Binary;
using Enigma.Serialization.Json;
using Xunit;

namespace Enigma.Test.Serialization.Json
{
    public class JsonSerializationTestContext
    {
        private readonly JsonEncoding _encoding;
        private readonly IFieldNameResolver _fieldNameResolver;

        public JsonSerializationTestContext()
        {
            _encoding = JsonEncoding.Unicode;
            _fieldNameResolver = new CamelCaseFieldNameResolver();
        }

        public void AssertWriteVisitorCall(string expected, Action<JsonWriteVisitor> action)
        {
            using (var memStream = new MemoryStream()) {
                var buffer = new BinaryBuffer(1024, memStream);
                var writeVisitor = new JsonWriteVisitor(_encoding, _fieldNameResolver, buffer);
                action(writeVisitor);
                buffer.Flush();
                var actual = _encoding.BaseEncoding.GetString(memStream.ToArray());
                Assert.Equal(expected, actual);
            }
        }

    }
}