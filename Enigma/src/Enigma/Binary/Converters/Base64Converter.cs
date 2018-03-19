using System.IO;
using System.Text;

namespace Enigma.Binary.Converters
{
    public class Base64Converter
    {
        public static Base64Converter UTF8 { get; } = new Base64Converter(Encoding.UTF8);
        public static Base64Converter UTF16BE { get; } = new Base64Converter(Encoding.BigEndianUnicode);
        public static Base64Converter UTF16LE { get; } = new Base64Converter(Encoding.Unicode);

        private readonly Base64BufferEncoder _encoder;
        private readonly Base64BufferDecoder _decoder;

        public Base64Converter(Encoding encoding)
        {
            var charSet = new Base64EncodedCharacterSet(encoding);
            _encoder = new Base64BufferEncoder(charSet);
            _decoder = new Base64BufferDecoder(charSet);
        }

        public int GetEncodedSizeOf(byte[] source)
        {
            return _encoder.GetSizeOf(source.Length);
        }

        public int GetEncodedSizeOf(int count)
        {
            return _encoder.GetSizeOf(count);
        }

        public void ConvertTo(byte[] source, int sourceOffset, int sourceCount, byte[] target, int targetOffset)
        {
            _encoder.Encode(source, sourceOffset, sourceCount, target, targetOffset);
        }

        public void ConvertFrom(byte[] source, int sourceOffset, int sourceCount, byte[] target, int targetOffset)
        {
            _decoder.Decode(source, sourceOffset, sourceCount, target, targetOffset);
        }

    }
}
