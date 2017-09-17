using System.IO;
using System.Reflection;
using Enigma.Serialization;
using Xunit;

namespace Enigma.Test.Serialization
{
    public abstract class SerializationTestContext
    {

        protected SerializationTestContext()
        {
        }

        protected abstract ITypedSerializer<T> CreateSerializer<T>();

        public T SerializeAndDeserialize<T>(T graph)
        {
            var serializer = CreateSerializer<T>();
            using (var stream = new MemoryStream()) {
                serializer.Serialize(stream, graph);
                stream.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(stream);
            }
        }

        public T AssertBinarySingleProperty<T>(T graph)
        {
            var actual = SerializeAndDeserialize(graph);

            var type = typeof (T);
            var property = type.GetProperty("Value");
            if (property != null) {
                var expectedValue = property.GetValue(graph);
                var actualValue = property.GetValue(actual);

                Assert.Equal(expectedValue, actualValue);
            }

            return actual;
        }

    }
}
