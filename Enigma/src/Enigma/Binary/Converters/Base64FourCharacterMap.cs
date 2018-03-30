﻿namespace Enigma.Binary.Converters
{
    public class Base64FourCharacterMap : IBase64CharacterMap
    {
        private readonly byte[][][][] _map;

        public Base64FourCharacterMap(Base64EncodedCharacterSet charSet)
        {
            const byte maxSize = 123;
            _map = new byte[maxSize][][][];
            var chars = charSet.Chars;
            byte v = 0;
            for (var i = 0; i < chars.Length; i += 4) {
                var midx = chars[i];
                var m1 = _map[midx];
                if (m1 == null) {
                    m1 = new byte[maxSize][][];
                    _map[midx] = m1;
                }

                midx = chars[i + 1];
                var m2 = m1[midx];
                if (m2 == null) {
                    m2 = new byte[maxSize][];
                    m1[midx] = m2;
                }

                midx = chars[i + 2];
                var m3 = m2[midx];
                if (m3 == null) {
                    m3 = new byte[maxSize];
                    m2[midx] = m3;
                }

                m3[chars[i + 3]] = v++;
            }
        }

        public unsafe void MapTo(ref byte* s, ref byte* t)
        {
            var b1 = _map[*s++][*s++][*s++][*s++];
            var b2 = _map[*s++][*s++][*s++][*s++];

            *t++ = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));
            b1 = _map[*s++][*s++][*s++][*s++];
            *t++ = (byte)(((b1 & 0x3C) >> 2) | ((b2 & 0x0F) << 4));
            b2 = _map[*s++][*s++][*s++][*s++];
            *t++ = (byte)(((b1 & 0x03) << 6) | b2);
        }

        public unsafe void MapLast(ref byte* s, ref byte* t, ref int padding)
        {
            var b1 = _map[*s++][*s++][*s++][*s++];
            var b2 = _map[*s++][*s++][*s++][*s++];

            *t++ = (byte)((b1 << 2) | ((b2 & 0x30) >> 4));

            b1 = _map[*s++][*s++][*s++][*s++];

            if (padding != 2) {
                *t++ = (byte)(((b1 & 0x3C) >> 2) | ((b2 & 0x0F) << 4));
            }

            b2 = _map[*s++][*s++][*s++][*s++];
            if (padding == 0) {
                *t++ = (byte)(((b1 & 0x03) << 6) | b2);
            }
        }
    }
}