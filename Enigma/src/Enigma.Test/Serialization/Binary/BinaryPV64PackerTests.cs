using System;
using System.IO;
using Enigma.Binary;
using Xunit;

namespace Enigma.Test.Serialization.Binary
{
    
    public class BinaryPV64PackerTests
    {
        private static void AssertPackDU(UInt64 value, byte expectedLength)
        {
            var actualLength = BinaryPV64Packer.GetULength(value);
            Assert.Equal(expectedLength, actualLength);
            using (var stream = new MemoryStream()) {
                BinaryPV64Packer.PackU(stream, value, actualLength);
                stream.Seek(0, SeekOrigin.Begin);
                var actual = BinaryPV64Packer.UnpackU(stream, actualLength);
                Assert.Equal(value, actual);
            }
        }

        [Fact]
        public void Pack64DUTest()
        {
            AssertPackDU(0xF5A38F, 3);
        }

        [Fact]
        public void Pack64HighDUTest()
        {
            AssertPackDU(UInt64.MaxValue, 8);
        }

        [Fact]
        public void Pack64LowDUTest()
        {
            AssertPackDU(UInt64.MinValue, 1);
        }

        private static void AssertPackDS(Int64 value, byte expectedLength)
        {
            var actualLength = BinaryPV64Packer.GetSLength(value);
            Assert.Equal(expectedLength, actualLength);
            using (var stream = new MemoryStream()) {
                BinaryPV64Packer.PackS(stream, value, actualLength);
                stream.Seek(0, SeekOrigin.Begin);
                var actual = BinaryPV64Packer.UnpackS(stream, actualLength);
                Assert.Equal(value, actual);
            }
        }

        [Fact]
        public void Pack64DSTest()
        {
            AssertPackDS(0xF5A38F, 4);
        }

        [Fact]
        public void Pack64HighDSTest()
        {
            AssertPackDS(Int64.MaxValue, 8);
        }

        [Fact]
        public void Pack64LowDSTest()
        {
            AssertPackDS(Int64.MinValue, 8);
        }
    }
}