using System;
using System.Collections.Generic;
using Enigma.Binary;
using Enigma.IO;

namespace Enigma.Serialization.Json
{
    public class JsonWriteVisitor : IWriteVisitor
    {
        private readonly JsonEncoding _encoding;
        private readonly IFieldNameResolver _fieldNameResolver;
        private readonly BinaryDataWriter _writer;
        private readonly BinaryBuffer _buffer;
        private readonly Stack<bool> _isFirsts;

        public JsonWriteVisitor(JsonEncoding encoding,
            IFieldNameResolver fieldNameResolver,
            BinaryBuffer buffer)
        {
            _encoding = encoding;
            _fieldNameResolver = fieldNameResolver;
            _buffer = buffer;
            _writer = new BinaryDataWriter(_buffer, _encoding.BaseEncoding);
            _isFirsts = new Stack<bool>();
        }

        private void WriteFieldName(VisitArgs args)
        {
            WriteEscaped(_fieldNameResolver.Resolve(args));
        }

        private void WriteValuePrefix(VisitArgs args)
        {
            switch (args.Type) {
                case LevelType.CollectionItem:
                case LevelType.DictionaryKey:
                case LevelType.Value:
                    EnsureComma();
                    break;
            }
            if (args.Type == LevelType.Value) {
                WriteFieldName(args);
                _writer.Write(_encoding.Assignment);
            }
        }

        private void WriteValueSuffix(VisitArgs args)
        {
            switch (args.Type) {
                case LevelType.DictionaryKey:
                    _writer.Write(_encoding.Assignment);
                    return;
            }
        }

        private void WriteValueDefault<T>(T? value, VisitArgs args)
            where T : struct, IConvertible
        {
            WriteValuePrefix(args);
            if (value == null) {
                _writer.Write(_encoding.Null);
            }
            else {
                _writer.Write(value.Value.ToString(JsonEncoding.NumberFormat));
            }
            WriteValueSuffix(args);
        }

        private void EnsureComma()
        {
            if (_isFirsts.Peek()) {
                _isFirsts.Pop();
                _isFirsts.Push(false);
            }
            else {
                _writer.Write(_encoding.Comma);
            }
        }

        private void WriteEscaped(string value)
        {
            if (value == null) {
                _writer.Write(_encoding.Null);
                return;
            }
            _writer.Write(_encoding.Quote);
            if (value.Length == 0) {
                _writer.Write(_encoding.Quote);
                return;
            }
            var index = 0;
            var position = 0;
            for (var i = 0; i < value.Length; i++) {
                if (!_encoding.RequiresEscape(value[i], out byte[] charBytes)) {
                    continue;
                }
                if (position == 0) {
                    var maxSize = _encoding.BaseEncoding.GetMaxByteCount(value.Length) + value.Length;
                    _buffer.RequestSpace(maxSize);
                    position = _buffer.Position;
                }
                if (index < i - 1) {
                    position += _encoding.BaseEncoding.GetBytes(value, index, i - index, _buffer.Buffer, position);
                }
                index = i + 1;
                Buffer.BlockCopy(_encoding.ReverseSolidus, 0, _buffer.Buffer, position, _encoding.ReverseSolidus.Length);
                position += _encoding.ReverseSolidus.Length;
                Buffer.BlockCopy(charBytes, 0, _buffer.Buffer, position, charBytes.Length);
                position += charBytes.Length;
            }
            if (index == 0) {
                _writer.Write(value);
                _writer.Write(_encoding.Quote);
                return;
            }

            if (index < value.Length) {
                position += _encoding.BaseEncoding.GetBytes(value, index, value.Length, _buffer.Buffer, position);
            }

            _buffer.Advance(position - _buffer.Position);
            _writer.Write(_encoding.Quote);
        }

        public void Visit(object level, VisitArgs args)
        {
            if (level == null) {
                _writer.Write(_encoding.Null);
                return;
            }

            _isFirsts.Push(true);
            switch (args.Type) {
                case LevelType.Single:
                    if (!args.IsRoot) {
                        WriteFieldName(args);
                        _writer.Write(_encoding.Assignment);
                    }
                    _writer.Write(_encoding.ObjectBegin);
                    return;
                case LevelType.Collection:
                    if (!args.IsRoot) {
                        WriteFieldName(args);
                        _writer.Write(_encoding.Assignment);
                    }
                    _writer.Write(_encoding.ArrayBegin);
                    return;
                case LevelType.Dictionary:
                    if (!args.IsRoot) {
                        WriteFieldName(args);
                        _writer.Write(_encoding.Assignment);
                    }
                    _writer.Write(_encoding.ObjectBegin);
                    return;
                case LevelType.CollectionInCollection:
                case LevelType.CollectionInDictionaryKey:
                case LevelType.CollectionInDictionaryValue: {
                    _writer.Write(_encoding.ArrayBegin);
                    return;
                }
            }
            _writer.Write(_encoding.ObjectBegin);
        }

        public void Leave(object level, VisitArgs args)
        {
            if (level == null) {
                return;
            }

            _isFirsts.Pop();
            switch (args.Type) {
                case LevelType.Collection:
                case LevelType.CollectionInCollection:
                case LevelType.CollectionInDictionaryKey:
                case LevelType.CollectionInDictionaryValue: {
                    _writer.Write(_encoding.ArrayBegin);

                    return;
                }
            }
            _writer.Write(_encoding.ObjectEnd);
        }

        public void VisitValue(byte? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(short? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(int? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(long? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(ushort? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(uint? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(ulong? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(bool? value, VisitArgs args)
        {
            WriteValuePrefix(args);
            if (value == null) {
                _writer.Write(_encoding.Null);
            }
            else {
                _writer.Write(value.Value ? _encoding.True : _encoding.False);
            }
            WriteValueSuffix(args);
        }

        public void VisitValue(float? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(double? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(decimal? value, VisitArgs args)
        {
            WriteValueDefault(value, args);
        }

        public void VisitValue(TimeSpan? value, VisitArgs args)
        {
            WriteValuePrefix(args);
            if (value == null) {
                _writer.Write(_encoding.Null);
            }
            else {
                _writer.Write(_encoding.Quote);
                _writer.Write(value.Value.ToString());
                _writer.Write(_encoding.Quote);
            }
            WriteValueSuffix(args);
        }

        public void VisitValue(DateTime? value, VisitArgs args)
        {
            WriteValuePrefix(args);
            if (value == null) {
                _writer.Write(_encoding.Null);
            }
            else {
                _writer.Write(_encoding.Quote);
                _writer.Write(value.Value.ToString("O", JsonEncoding.NumberFormat));
                _writer.Write(_encoding.Quote);
            }
            WriteValueSuffix(args);
        }

        public void VisitValue(string value, VisitArgs args)
        {
            WriteValuePrefix(args);
            WriteEscaped(value);
            WriteValueSuffix(args);
        }

        public void VisitValue(Guid? value, VisitArgs args)
        {
            WriteValuePrefix(args);
            if (value == null) {
                _writer.Write(_encoding.Null);
            }
            else {
                _writer.Write(_encoding.Quote);
                _writer.Write(value.Value.ToString());
                _writer.Write(_encoding.Quote);
            }
            WriteValueSuffix(args);
        }

        public void VisitValue(byte[] value, VisitArgs args)
        {
            WriteValuePrefix(args);

            _writer.Write(_encoding.ArrayBegin);
            var isFirst = true;
            foreach (var el in value) {
                if (isFirst) {
                    isFirst = false;
                }
                else {
                    _writer.Write(_encoding.Comma);
                }
                _writer.Write(el.ToString());
            }
            _writer.Write(_encoding.ArrayEnd);

            WriteValueSuffix(args);
        }

    }
}