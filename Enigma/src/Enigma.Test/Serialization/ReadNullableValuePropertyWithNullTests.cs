using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization
{
    
    public class ReadNullableValuePropertyWithNullTests
    {

        [Fact]
        public void ReadInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableInt16Graph>();
        }

        [Fact]
        public void ReadInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableInt32Graph>();
        }

        [Fact]
        public void ReadInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableInt64Graph>();
        }

        [Fact]
        public void ReadUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableUInt16Graph>();
        }

        [Fact]
        public void ReadUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableUInt32Graph>();
        }

        [Fact]
        public void ReadUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableUInt64Graph>();
        }

        [Fact]
        public void ReadBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableBooleanGraph>();
        }

        [Fact]
        public void ReadSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableSingleGraph>();
        }

        [Fact]
        public void ReadDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableDoubleGraph>();
        }

        [Fact]
        public void ReadDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableDecimalGraph>();
        }

        [Fact]
        public void ReadTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableTimeSpanGraph>();
        }

        [Fact]
        public void ReadDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableDateTimeGraph>();
        }

        [Fact]
        public void ReadGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableGuidGraph>();
        }

        [Fact]
        public void ReadEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSinglePropertyWithNull<NullableEnumGraph>();
        }

    }
}