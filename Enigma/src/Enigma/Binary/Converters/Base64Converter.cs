﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
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

        public int GetDecodedSizeOf(byte[] source)
        {
            return _decoder.GetSizeOf(source, 0, source.Length);
        }

        public int GetDecodedSizeOf(byte[] source, int offset, int count)
        {
            return _decoder.GetSizeOf(source, offset, count);
        }

        public void Encode(byte[] source, int sourceOffset, int sourceCount, byte[] target, int targetOffset)
        {
            _encoder.Encode(source, sourceOffset, sourceCount, target, targetOffset);
        }

        public void Decode(byte[] source, int sourceOffset, int sourceCount, byte[] target, int targetOffset)
        {
            _decoder.Decode(source, sourceOffset, sourceCount, target, targetOffset);
        }

    }
}
