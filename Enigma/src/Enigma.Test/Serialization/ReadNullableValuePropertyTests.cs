using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization
{
    
    public class ReadNullableValuePropertyTests
    {

        [Fact]
        public void ReadInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableInt16Graph>();
        }

        [Fact]
        public void ReadInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableInt32Graph>();
        }

        [Fact]
        public void ReadInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableInt64Graph>();
        }

        [Fact]
        public void ReadUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableUInt16Graph>();
        }

        [Fact]
        public void ReadUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableUInt32Graph>();
        }

        [Fact]
        public void ReadUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableUInt64Graph>();
        }

        [Fact]
        public void ReadBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableBooleanGraph>();
        }

        [Fact]
        public void ReadSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableSingleGraph>();
        }

        [Fact]
        public void ReadDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableDoubleGraph>();
        }

        [Fact]
        public void ReadDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableDecimalGraph>();
        }

        [Fact]
        public void ReadTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableTimeSpanGraph>();
        }

        [Fact]
        public void ReadDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableDateTimeGraph>();
        }

        [Fact]
        public void ReadGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableGuidGraph>();
        }

        [Fact]
        public void ReadEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertReadSingleProperty<NullableEnumGraph>();
        }

    }
}