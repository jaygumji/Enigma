﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using Enigma.Binary;
using Enigma.Modelling;
using System;
using System.IO;
using ProtoBuf.Meta;

namespace Enigma.ProtocolBuffer
{
    public class ProtocolBufferBinaryConverter<T> : IBinaryConverter<T>
    {

        private readonly TypeModel _typeModel;

        public ProtocolBufferBinaryConverter()
        {
            _typeModel = TypeModelBuilder.Create<T>();
        }

        public ProtocolBufferBinaryConverter(Model model)
        {
            _typeModel = TypeModelBuilder.Create(model);
        }

        public T Convert(byte[] value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, 0, value.Length);
        }

        public T Convert(byte[] value, int startIndex)
        {
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, 0, value.Length - startIndex);
        }

        public T Convert(byte[] value, int startIndex, int length)
        {
            if (value == null) throw new ArgumentNullException("value");
            using (var stream = new MemoryStream(value, startIndex, length))
            {
                var instance = Activator.CreateInstance<T>();
                _typeModel.Deserialize(stream, instance, typeof(T));
                return instance;
            }
        }

        public byte[] Convert(T value)
        {
            using (var stream = new MemoryStream())
            {
                _typeModel.Serialize(stream, value);
                return stream.ToArray();
            }
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
            if (value == null) throw new ArgumentNullException("value");
            return Convert(value, startIndex, length);
        }

        public byte[] Convert(object value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (!(value is T)) throw new ArgumentException("Invalid type of value");

            return Convert((T)value);
        }

        public void Convert(T value, byte[] buffer)
        {
            Convert(value, buffer, 0);
        }

        public void Convert(T value, byte[] buffer, int offset)
        {
            using (var stream = new MemoryStream(buffer)) {
                stream.Seek(offset, SeekOrigin.Begin);
                _typeModel.Serialize(stream, value);
            }
        }

        public void Convert(object value, byte[] buffer)
        {
            Convert((T) value, buffer, 0);
        }

        public void Convert(object value, byte[] buffer, int offset)
        {
            Convert((T)value, buffer, offset);
        }

        public void Convert(T value, BinaryWriteBuffer writeBuffer)
        {
            byte[] data;
            using (var stream = new MemoryStream())
            {
                _typeModel.Serialize(stream, value);
                data = stream.ToArray();
            }
            writeBuffer.Write(data, 0, data.Length);
        }

        void IBinaryConverter.Convert(object value, BinaryWriteBuffer writeBuffer)
        {
            Convert((T)value, writeBuffer);
        }

    }
}
