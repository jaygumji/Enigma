using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Enigma.Binary;
using Enigma.IO;

namespace Enigma.Serialization.PackedBinary
{
    public sealed class PackedDataWriteVisitor : IWriteVisitor
    {

        private static readonly Encoding Encoding = Encoding.UTF8;
        private readonly BinaryDataWriter _writer;
        private readonly Stack<BinaryBufferReservation> _reservations;
        private readonly BinaryBuffer _buffer;

        public PackedDataWriteVisitor(BinaryBuffer buffer)
        {
            _buffer = buffer;
            _writer = new BinaryDataWriter(_buffer);
            _reservations = new Stack<BinaryBufferReservation>();
        }

        public void Visit(object level, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (level == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte(BinaryZPacker.VariabelLength);

            if (args.IsRoot) {
                return;
            }

            _reservations.Push(_writer.Reserve());
        }

        public void Leave(object level, VisitArgs args)
        {
            if (args.IsRoot) {
                return;
            }

            if (level != null) {
                var reservation = _reservations.Pop();
                _writer.Write(reservation);
                BinaryZPacker.Pack(_buffer, 0);
            }
        }

        public void VisitValue(byte? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((Byte)BinaryInformation.Byte.FixedLength);
            _buffer.WriteByte(value.Value);
        }

        public void VisitValue(short? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetSLength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackS(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(int? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetSLength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackS(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(long? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetSLength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackS(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(ushort? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetULength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackU(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(uint? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetULength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackU(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(ulong? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetULength(value.Value);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackU(_buffer.Buffer, offset, value.Value, length);
        }

        public void VisitValue(bool? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((Byte)BinaryInformation.Boolean.FixedLength);
            var bytes = BinaryInformation.Boolean.Converter.Convert(value.Value);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        public void VisitValue(float? value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((Byte)BinaryInformation.Single.FixedLength);
            var bytes = BinaryInformation.Single.Converter.Convert(value.Value);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        public void VisitValue(double? value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((Byte)BinaryInformation.Double.FixedLength);
            var bytes = BinaryInformation.Double.Converter.Convert(value.Value);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        public void VisitValue(decimal? value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((Byte)BinaryInformation.Decimal.FixedLength);
            var bytes = BinaryInformation.Decimal.Converter.Convert(value.Value);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        public void VisitValue(TimeSpan? value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = BinaryPV64Packer.GetSLength(value.Value.Ticks);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackS(_buffer.Buffer, offset, value.Value.Ticks, length);
        }

        public void VisitValue(DateTime? value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            if (value.Value.Kind != DateTimeKind.Utc)
                value = value.Value.ToUniversalTime();

            var length = BinaryPV64Packer.GetSLength(value.Value.Ticks);
            _buffer.WriteByte(length);
            var offset = _buffer.Advance(length);
            BinaryPV64Packer.PackS(_buffer.Buffer, offset, value.Value.Ticks, length);
        }

        public void VisitValue(string value, VisitArgs args)
        {
            if (args.Index > 0) {
                BinaryZPacker.Pack(_buffer, args.Index);
            }

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            var length = Encoding.GetByteCount(value);

            if (length < BinaryZPacker.VariabelLength)
                _buffer.WriteByte((byte)length);
            else {
                _buffer.WriteByte(BinaryZPacker.VariabelLength);
                BinaryV32Packer.PackU(_buffer, (uint)value.Length);
            }

            var position = _buffer.Advance(length);
            Encoding.GetBytes(value, 0, value.Length, _buffer.Buffer, position);
        }

        public void VisitValue(Guid? value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            _buffer.WriteByte((byte)BinaryInformation.Guid.FixedLength);
            var bytes = BinaryInformation.Guid.Converter.Convert(value.Value);
            _buffer.Write(bytes, 0, bytes.Length);
        }

        public void VisitValue(byte[] value, VisitArgs args)
        {
            if (args.Index > 0)
                BinaryZPacker.Pack(_buffer, args.Index);

            if (value == null) {
                _buffer.WriteByte(BinaryZPacker.Null);
                return;
            }

            if (value.Length < BinaryZPacker.VariabelLength)
                _buffer.WriteByte((Byte) value.Length);
            else {
                _buffer.WriteByte(BinaryZPacker.VariabelLength);
                BinaryV32Packer.PackU(_buffer, (uint)value.Length);
            }
            _buffer.Write(value, 0, value.Length);
        }

    }
}