using System.IO;
using System.Linq;
using System.Reflection;
using Enigma.Binary;
using Enigma.IO;
using Enigma.Serialization;
using Enigma.Serialization.PackedBinary;
using Enigma.Serialization.Reflection;
using Enigma.Serialization.Reflection.Emit;
using Enigma.Test.Serialization.Fakes;
using Enigma.Test.Serialization.HardCoded;
using Enigma.Testing.Fakes.Entities;
using Xunit;
using Enigma.Reflection;

namespace Enigma.Test.Serialization
{
    public class SerializationTestContext
    {
        private readonly DynamicTravellerContext _travellerContext;

        public SerializationTestContext()
        {
            _travellerContext = new DynamicTravellerContext(new SerializableTypeProvider(new SerializationReflectionInspector(), FactoryTypeProvider.Instance));
        }

        public IGraphTraveller<T> CreateTraveller<T>()
        {
            var traveller = _travellerContext.GetInstance<T>();
            return traveller;
        }

        public byte[] Pack<T>(T graph)
        {
            var stream = new MemoryStream();
            using (var buffer = new BinaryBuffer(1024, stream)) {
                var visitor = new PackedDataWriteVisitor(buffer);

                var traveller = CreateTraveller<T>();
                traveller.Travel(visitor, graph);
            }
            return stream.ToArray();
        }

        public T SerializeAndDeserialize<T>(T graph)
        {
            var serializer = new PackedDataSerializer<T>();
            using (var stream = new MemoryStream()) {
                serializer.Serialize(stream, graph);
                stream.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(stream);
            }
        }

        public void AssertWriteSingleProperty<T>(T graph)
        {
            var stats = AssertWrite(1, graph);
            stats.AssertVisitOrderExact(LevelType.Value);
        }

        public void AssertReadSingleProperty<T>() where T : new()
        {
            var stats = AssertRead<T>(1);
            stats.AssertVisitOrderExact(LevelType.Value);
        }

        public void AssertReadSinglePropertyWithNull<T>() where T : new()
        {
            var stats = AssertRead<T>(1, -1, readOnlyNull: true);
            stats.AssertVisitOrderExact(LevelType.Value);
        }

        public IWriteStatistics AssertWrite<T>(int expectedValueWriteCount, T graph)
        {
            var visitor = new FakeWriteVisitor();
            var traveller = CreateTraveller<T>();
            traveller.Travel(visitor, graph);

            //_travellerContext.Save();
            visitor.Statistics.AssertHiearchy();
            Assert.Equal(expectedValueWriteCount, visitor.Statistics.VisitValueCount);

            return visitor.Statistics;
        }

        public IReadStatistics AssertRead<T>(int expectedValueReadCount) where T : new()
        {
            return AssertRead<T>(expectedValueReadCount, -1);
        }

        public IReadStatistics AssertRead<T>(int expectedValueReadCount, int allowedVisitCount) where T : new()
        {
            return AssertRead<T>(expectedValueReadCount, allowedVisitCount, readOnlyNull: false);
        }

        public IReadStatistics AssertRead<T>(int expectedValueReadCount, int allowedVisitCount, bool readOnlyNull) where T : new()
        {
            var visitor = new FakeReadVisitor {AllowedVisitCount = allowedVisitCount, ReadOnlyNull = readOnlyNull};
            var traveller = CreateTraveller<T>();

            var graph = new T();
            traveller.Travel(visitor, graph);

            visitor.Statistics.AssertHiearchy();
            Assert.Equal(expectedValueReadCount, visitor.Statistics.VisitValueCount);

            return visitor.Statistics;
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

        public static string GetFilledDataBlockHexString()
        {
            var bytes = GetFilledDataBlockBlob();
            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);
            var hex = "0x" + string.Join("", bytes.Select(b => b.ToString("X")));
            Assert.NotNull(hex);
            return hex;
        }

        public static byte[] GetFilledDataBlockBlob()
        {
            var stream = new MemoryStream();
            using (var buffer = new BinaryBuffer(1024, stream)) {
                var visitor = new PackedDataWriteVisitor(buffer);
                var traveller = DataBlockHardCodedTraveller.Create();
                traveller.Travel(visitor, DataBlock.Filled());
            }

            var bytes = stream.ToArray();
            return bytes;
        }

    }
}
