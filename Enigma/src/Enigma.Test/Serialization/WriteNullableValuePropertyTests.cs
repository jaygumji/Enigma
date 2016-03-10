using System;
using Enigma.Testing.Fakes.Entities;
using Enigma.Testing.Fakes.Graphs;
using Xunit;


namespace Enigma.Test.Serialization
{
    
    public class WriteNullableValuePropertyTests
    {
        [Fact]
        public void WriteNullableInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt16Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt32Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableInt64Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableUInt16Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt16Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableUInt32Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt32Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableUInt64Test()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableUInt64Graph { Value = 42 });
        }

        [Fact]
        public void WriteNullableBooleanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableBooleanGraph { Value = true });
        }

        [Fact]
        public void WriteNullableSingleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableSingleGraph { Value = 42.3f });
        }

        [Fact]
        public void WriteNullableDoubleTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDoubleGraph { Value = 42.7d });
        }

        [Fact]
        public void WriteNullableDecimalTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDecimalGraph { Value = 42.5434M });
        }

        [Fact]
        public void WriteNullableTimeSpanTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableTimeSpanGraph { Value = new TimeSpan(12,30,00) });
        }

        [Fact]
        public void WriteNullableDateTimeTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableDateTimeGraph { Value = new DateTime(2001, 01, 07, 15, 30, 24) });
        }

        [Fact]
        public void WriteNullableGuidTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableGuidGraph { Value = Guid.Empty });
        }

        [Fact]
        public void WriteNullableEnumTest()
        {
            var context = new SerializationTestContext();
            context.AssertWriteSingleProperty(new NullableEnumGraph { Value = ApplicationType.Api });
        }

    }

}