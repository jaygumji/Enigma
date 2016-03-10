using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization
{
    
    public class WriteNullableValuePropertyWithNullTests
    {
        [Fact]
        public void WriteNullableInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt16Graph { Value = null });
        }

        [Fact]
        public void WriteNullableInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt32Graph { Value = null });
        }

        [Fact]
        public void WriteNullableInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt64Graph { Value = null });
        }

        [Fact]
        public void WriteNullableUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt16Graph { Value = null });
        }

        [Fact]
        public void WriteNullableUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt32Graph { Value = null });
        }

        [Fact]
        public void WriteNullableUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt64Graph { Value = null });
        }

        [Fact]
        public void WriteNullableBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableBooleanGraph { Value = null });
        }

        [Fact]
        public void WriteNullableSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableSingleGraph { Value = null });
        }

        [Fact]
        public void WriteNullableDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDoubleGraph { Value = null });
        }

        [Fact]
        public void WriteNullableDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDecimalGraph { Value = null });
        }

        [Fact]
        public void WriteNullableTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableTimeSpanGraph { Value = null });
        }

        [Fact]
        public void WriteNullableDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDateTimeGraph { Value = null });
        }

        [Fact]
        public void WriteNullableGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableGuidGraph { Value = null });
        }

        [Fact]
        public void WriteNullableEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableEnumGraph { Value = null });
        }

    }
}