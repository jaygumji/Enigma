using System;
using System.Linq;
using System.Text;
using Enigma.Binary.Converters;
using Xunit;

namespace Enigma.Test.Binary.Converters
{
    public class Base64ConverterTests
    {

        [Fact]
        public void EncodeUtf8()
        {
            var converter = Base64Converter.UTF8;
            var source = Enumerable.Range(byte.MinValue, byte.MaxValue)
                .Select(i => (byte)i)
                .ToArray();
            var target = new byte[converter.GetEncodedSizeOf(source)];
            converter.ConvertTo(source, 0, source.Length, target, 0);

            var base64 = Convert.ToBase64String(source);
            var actual = Encoding.UTF8.GetString(target);

            Assert.Equal(base64, actual);
        }

        [Fact]
        public void EncodeUtf16LE()
        {
            var converter = Base64Converter.UTF16LE;
            var source = Enumerable.Range(byte.MinValue, byte.MaxValue)
                .Select(i => (byte)i)
                .ToArray();
            var target = new byte[converter.GetEncodedSizeOf(source)];
            converter.ConvertTo(source, 0, source.Length, target, 0);

            var base64 = Convert.ToBase64String(source);
            var actual = Encoding.Unicode.GetString(target);

            Assert.Equal(base64, actual);
        }

        [Fact]
        public void EncodeUtf16BE()
        {
            var converter = Base64Converter.UTF16BE;
            var source = Enumerable.Range(byte.MinValue, byte.MaxValue)
                .Select(i => (byte)i)
                .ToArray();
            var target = new byte[converter.GetEncodedSizeOf(source)];
            converter.ConvertTo(source, 0, source.Length, target, 0);

            var base64 = Convert.ToBase64String(source);
            var actual = Encoding.BigEndianUnicode.GetString(target);

            Assert.Equal(base64, actual);
        }


    }
}
