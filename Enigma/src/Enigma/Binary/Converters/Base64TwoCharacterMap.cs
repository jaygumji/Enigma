namespace Enigma.Binary.Converters
{
    public class Base64TwoCharacterMap : IBase64CharacterMap
    {
        private readonly byte[][] _map;

        public Base64TwoCharacterMap(Base64EncodedCharacterSet charSet)
        {
            _map = new byte[byte.MaxValue][];
            var chars = charSet.Chars;
            for (byte i = 0; i < chars.Length; i+=2) {
                var xi = chars[i];
                var x = _map[xi];
                if (x == null) {
                    x = new byte[byte.MaxValue];
                    _map[xi] = x;
                }
                x[chars[i+1]] = i;
            }
        }

        public unsafe void MapTo(ref byte* s, ref byte* t)
        {
            var b1 = _map[*s++][*s++];
            var b2 = _map[*s++][*s++];

            *t++ = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));
            b1 = _map[*s++][*s++];
            *t++ = (byte)(((b1 & 0x3C) >> 2) | ((b2 & 0x0F) << 4));
            b2 = _map[*s++][*s++];
            *t++ = (byte)(((b1 & 0x03) << 6) | b2);
        }

        public unsafe void MapLast(ref byte* s, ref byte* t, ref int padding)
        {
            var b1 = _map[*s++][*s++];
            var b2 = _map[*s++][*s++];

            *t++ = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));

            b1 = _map[*s++][*s++];

            if (padding != 2) {
                *t++ = (byte)(((b1 & 0x3C) >> 2) | ((b2 & 0x0F) << 4));
            }

            b2 = _map[*s++][*s++];
            if (padding == 0) {
                *t++ = (byte)(((b1 & 0x03) << 6) | b2);
            }
        }

    }
}