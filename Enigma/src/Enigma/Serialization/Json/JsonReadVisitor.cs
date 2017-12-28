using System;
using System.IO;
using System.Text;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{
    public class JsonReadVisitor : IReadVisitor
    {
        private readonly JsonEncoding _encoding;
        private readonly IFieldNameResolver _fieldNameResolver;
        private readonly BinaryReadBuffer _buffer;
        private readonly JsonReader _reader;

        public JsonReadVisitor(JsonEncoding encoding, IFieldNameResolver fieldNameResolver, BinaryReadBuffer buffer)
        {
            _encoding = encoding;
            _fieldNameResolver = fieldNameResolver;
            _buffer = buffer;
            _reader = new JsonReader(buffer, _encoding);
        }

        //private string FindField(VisitArgs args)
        //{
        //    var fieldName = _reader.ReadString();
        //    _fieldNameResolver.Unresolve();
        //}

        public ValueState TryVisit(VisitArgs args)
        {
            var literal = _reader.PeekLiteral();

            if (args.IsRoot) {
                if (literal == JsonLiteral.Null) {
                    return ValueState.Null;
                }
                switch (args.Type) {
                    case LevelType.Dictionary:
                    case LevelType.Single:
                        if (literal == JsonLiteral.ObjectBegin) {
                            return ValueState.Found;
                        }
                        throw UnexpectedJsonException.From("object begin token", _buffer, _encoding);
                    case LevelType.Collection:
                        if (literal == JsonLiteral.ArrayBegin) {
                            return ValueState.Found;
                        }
                        throw UnexpectedJsonException.From("array begin token", _buffer, _encoding);
                }

            }

            switch (args.Type) {
            }

            throw new InvalidOperationException("Invalid root");
        }

        public void Leave(VisitArgs args)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out byte? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out short? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out int? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out long? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out ushort? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out uint? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out ulong? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out bool? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out float? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out double? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out decimal? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out TimeSpan? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out DateTime? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out string value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out Guid? value)
        {
            throw new NotImplementedException();
        }

        public bool TryVisitValue(VisitArgs args, out byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}