using System.IO;
using System.Linq;
using Enigma.IO;
using Enigma.Serialization.PackedBinary;
using Enigma.Serialization.Reflection.Emit;
using Enigma.Test.Serialization.HardCoded;
using Enigma.Testing.Fakes.Entities;
using Xunit;


namespace Enigma.Test.Serialization.Binary
{

    
    public class PackedDataReadVisitorTests
    {

        private static void AssertAreEqual(DataBlock expected, DataBlock actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Int16, actual.Int16);
            Assert.Equal(expected.Int32, actual.Int32);
            Assert.Equal(expected.Int64, actual.Int64);
            Assert.Equal(expected.UInt16, actual.UInt16);
            Assert.Equal(expected.UInt32, actual.UInt32);
            Assert.Equal(expected.UInt64, actual.UInt64);
            Assert.Equal(expected.Single, actual.Single);
            Assert.Equal(expected.Double, actual.Double);
            Assert.Equal(expected.Decimal, actual.Decimal);
            Assert.Equal(expected.TimeSpan, actual.TimeSpan);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.String, actual.String);
            Assert.Equal(expected.Boolean, actual.Boolean);
            Assert.Equal(expected.Byte, actual.Byte);
            Assert.True(expected.Blob.SequenceEqual(actual.Blob));

            Assert.True(expected.Messages.SequenceEqual(actual.Messages));
            Assert.True(expected.Stamps.SequenceEqual(actual.Stamps));

            Assert.NotNull(actual.Relation);
            Assert.Equal(expected.Relation.Id, actual.Relation.Id);
            Assert.Equal(expected.Relation.Name, actual.Relation.Name);
            Assert.Equal(expected.Relation.Description, actual.Relation.Description);
            Assert.Equal(expected.Relation.Value, actual.Relation.Value);
            Assert.Null(actual.DummyRelation);

            Assert.True(expected.IndexedValues.Keys.SequenceEqual(actual.IndexedValues.Keys));
            Assert.True(expected.IndexedValues.Values.SequenceEqual(actual.IndexedValues.Values));

            Assert.NotNull(actual.Categories);
            Assert.Equal(3, actual.Categories.Count);
            Assert.True(expected.Categories.Keys.SequenceEqual(actual.Categories.Keys));
            Assert.True(expected.Categories.Values.SequenceEqual(actual.Categories.Values));
        }

        [Fact]
        public void ReadHardCodedTravelTest()
        {
            var bytes = BinarySerializationTestContext.GetFilledDataBlockBlob();
            var stream = new MemoryStream(bytes);
            var visitor = new PackedDataReadVisitor(stream);

            var traveller = DataBlockHardCodedTraveller.Create();

            var graph = new DataBlock();
            traveller.Travel(visitor, graph);

            var expected = DataBlock.Filled();
            AssertAreEqual(expected, graph);
        }

        [Fact]
        public void ReadDynamicTravelTest()
        {
            var bytes = BinarySerializationTestContext.GetFilledDataBlockBlob();
            var stream = new MemoryStream(bytes);
            var visitor = new PackedDataReadVisitor(stream);

            var context = new DynamicTravellerContext();
            var traveller = context.GetInstance<DataBlock>();

            var graph = new DataBlock();
            traveller.Travel(visitor, graph);

            var expected = DataBlock.Filled();
            AssertAreEqual(expected, graph);
        }

    }
}
