﻿using System.Text;

namespace Enigma.Binary.Converters
{
    public sealed class Base64EncodedCharacterSet
    {
        private const string BaselineSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public byte[] Chars { get; }
        public byte[] PaddingChar { get; }
        public int CharSize { get; }

        public Base64EncodedCharacterSet(Encoding encoding)
        {
            Chars = encoding.GetBytes(BaselineSet);
            PaddingChar = encoding.GetBytes("=");
            CharSize = Chars.Length / 64;
        }
    }
}