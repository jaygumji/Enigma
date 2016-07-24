﻿using System;
using System.Text;

namespace Enigma.Binary.Converters
{
    public class BinaryConverterString : IBinaryConverter<String>
    {

        private static readonly Encoding Encoding = Encoding.UTF8;

        public String Convert(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, 0, value.Length);
        }

        public String Convert(byte[] value, int startIndex)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, startIndex, value.Length - startIndex);
        }

        public String Convert(byte[] value, int startIndex, int length)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Encoding.GetString(value, startIndex, length);
        }

        public byte[] Convert(String value)
        {
            return Encoding.GetBytes(value);
        }

        object IBinaryConverter.Convert(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, 0, value.Length);
        }

        object IBinaryConverter.Convert(byte[] value, int startIndex)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, startIndex, value.Length - startIndex);
        }

        object IBinaryConverter.Convert(byte[] value, int startIndex, int length)
        {
            return Convert(value, startIndex, length);
        }

        byte[] IBinaryConverter.Convert(object value)
        {
            return Convert((String)value);
        }

        public void Convert(String value, byte[] buffer)
        {
            Convert(value, buffer, 0);
        }

        public void Convert(String value, byte[] buffer, int offset)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (value == null || value.Length == 0) return;
            Encoding.GetBytes(value, 0, value.Length, buffer, offset);
        }

        void IBinaryConverter.Convert(object value, byte[] buffer)
        {
            Convert((String)value, buffer, 0);
        }

        void IBinaryConverter.Convert(object value, byte[] buffer, int offset)
        {
            Convert((String)value, buffer, offset);
        }

        public void Convert(String value, BinaryBuffer buffer)
        {
            var length = Encoding.GetByteCount(value);
            var offset = buffer.Advance(length);
            Convert(value, buffer.Buffer, offset);
        }

        void IBinaryConverter.Convert(object value, BinaryBuffer buffer)
        {
            Convert((String)value, buffer);
        }

    }
}
