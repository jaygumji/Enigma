using System;
using System.IO;
using Enigma.Binary;
using Xunit;


namespace Enigma.Test.Serialization.Binary
{
    
    public class BinaryV64PackerTests
    {

        private static void AssertPackU(UInt64? value)
        {
            using (var stream = new MemoryStream()) {
                BinaryV64Packer.PackU(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                var actual = BinaryV64Packer.UnpackU(stream);
                Assert.Equal(value, actual);
            }
        }

        private static void AssertPackS(Int64? value)
        {
            using (var stream = new MemoryStream()) {
                BinaryV64Packer.PackS(stream, value);
                stream.Seek(0, SeekOrigin.Begin);
                var actual = BinaryV64Packer.UnpackS(stream);
                Assert.Equal(value, actual);
            }
        }

        [Fact]
        public void Pack64NullUTest()
        {
            AssertPackU(null);
        }

        [Fact]
        public void Pack64NullSTest()
        {
            AssertPackS(null);
        }

        [Fact]
        public void Pack64UTest()
        {
            AssertPackU(0x0FC0D096U);
        }

        [Fact]
        public void Pack64STest()
        {
            AssertPackS(0x0FC0D096);
        }

        [Fact]
        public void Pack64HighUTest()
        {
            AssertPackU(UInt64.MaxValue);
        }

        [Fact]
        public void Pack64HighSTest()
        {
            AssertPackS(Int64.MaxValue);
        }

        [Fact]
        public void Pack64LowUTest()
        {
            AssertPackU(UInt64.MinValue);
        }

        [Fact]
        public void Pack64LowSTest()
        {
            AssertPackS(Int64.MinValue);
        }

    }
}