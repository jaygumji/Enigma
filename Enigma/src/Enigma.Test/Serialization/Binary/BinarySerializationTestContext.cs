using System.IO;
using System.Linq;
using Enigma.Binary;
using Enigma.Reflection;
using Enigma.Serialization;
using Enigma.Serialization.PackedBinary;
using Enigma.Serialization.Reflection;
using Enigma.Serialization.Reflection.Emit;
using Enigma.Test.Fakes.Entities;
using Enigma.Test.Serialization.HardCoded;
using Enigma.Testing.Fakes.Entities;
using Xunit;

namespace Enigma.Test.Serialization.Binary
{
    public class BinarySerializationTestContext : SerializationTestContext
    {

        private readonly DynamicTravellerContext _travellerContext;

        public BinarySerializationTestContext()
        {
            _travellerContext = new DynamicTravellerContext(new SerializableTypeProvider(new SerializationReflectionInspector(), FactoryTypeProvider.Instance));
        }

        public IGraphTraveller<T> CreateTraveller<T>()
        {
            var traveller = _travellerContext.GetInstance<T>();
            return traveller;
        }

        protected override ITypedSerializer<T> CreateSerializer<T>()
        {
            return new PackedDataSerializer<T>();
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
