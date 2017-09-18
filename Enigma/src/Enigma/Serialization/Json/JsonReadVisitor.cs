using System;
using System.IO;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{
    public class JsonReadVisitor : IReadVisitor
    {
        private readonly JsonEncoding _encoding;
        private readonly IFieldNameResolver _fieldNameResolver;
        private readonly Stream _stream;

        public JsonReadVisitor(JsonEncoding encoding, IFieldNameResolver fieldNameResolver, Stream stream)
        {
            _encoding = encoding;
            _fieldNameResolver = fieldNameResolver;
            _stream = stream;
        }

        public ValueState TryVisit(VisitArgs args)
        {
            throw new NotImplementedException();
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