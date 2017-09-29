using System.IO;
using System.Text;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{
    public class JsonReader
    {
        private readonly BinaryReadBuffer _buffer;
        private readonly JsonEncoding _encoding;

        public JsonReader(Stream stream)
            : this(stream, JsonEncoding.UTF16LE)
        {
        }

        public JsonReader(Stream stream, JsonEncoding encoding)
            : this(new BinaryReadBuffer(1024, stream), encoding)
        {
        }

        public JsonReader(BinaryReadBuffer buffer, JsonEncoding encoding)
        {
            _buffer = buffer;
            _encoding = encoding;
        }

        private bool IsLiteral(byte first, byte[] literalBytes, bool advance = true)
        {
            if (first != literalBytes[0]) {
                return false;
            }
            if (literalBytes.Length == 1) {
                return true;
            }
            if (literalBytes.Length == 2) {
                var b = _buffer.PeekByte(1);
                if (b != literalBytes[1]) {
                    return false;
                }
                if (advance) {
                    _buffer.Advance(1);
                }
                return true;
            }
            for (var i = 1; i < literalBytes.Length; i++) {
                if (_buffer.PeekByte(i) != literalBytes[i]) {
                    return false;
                }
            }
            if (advance) {
                _buffer.Advance(literalBytes.Length);
            }
            return true;
        }

        public JsonLiteral ReadLiteral()
        {
            var first = _buffer.PeekByte();

            if (IsLiteral(first, _encoding.ObjectBegin)) {
                return JsonLiteral.ObjectBegin;
            }
            if (IsLiteral(first, _encoding.ObjectEnd)) {
                return JsonLiteral.ObjectEnd;
            }
            if (IsLiteral(first, _encoding.ArrayBegin)) {
                return JsonLiteral.ArrayBegin;
            }
            if (IsLiteral(first, _encoding.ArrayEnd)) {
                return JsonLiteral.ArrayEnd;
            }
            if (IsLiteral(first, _encoding.Assignment)) {
                return JsonLiteral.Assignment;
            }
            if (IsLiteral(first, _encoding.Quote)) {
                return JsonLiteral.Quote;
            }
            if (IsLiteral(first, _encoding.Comma)) {
                return JsonLiteral.Comma;
            }
            if (IsLiteral(first, _encoding.Minus, advance:false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Zero, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.One, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Two, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Three, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Four, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Five, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Six, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Seven, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Eight, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Nine, advance: false)) {
                return JsonLiteral.Number;
            }
            if (IsLiteral(first, _encoding.Null)) {
                return JsonLiteral.Null;
            }
            if (IsLiteral(first, _encoding.True)) {
                return JsonLiteral.True;
            }
            if (IsLiteral(first, _encoding.False)) {
                return JsonLiteral.False;
            }
            if (IsLiteral(first, _encoding.Undefined)) {
                return JsonLiteral.Undefined;
            }
            throw UnexpectedJsonException.From("literal or value", _buffer, _encoding);
        }

        private bool IsNextCharacter(byte[] charCode, int offset)
        {
            if (_buffer.Buffer[offset] != charCode[0]) {
                return false;
            }
            if (charCode.Length == 1) {
                return true;
            }
            if (_buffer.Buffer[offset + 1] != charCode[1]) {
                return false;
            }
            if (charCode.Length == 2) {
                return true;
            }
            if (_buffer.Buffer[offset + 2] != charCode[2]) {
                return false;
            }
            if (charCode.Length == 3) {
                return true;
            }
            return _buffer.Buffer[offset + 3] == charCode[3];
        }

        private void AppendString(StringBuilder b, int offset)
        {
            if (_buffer.Position == offset) return;
            var length = offset - _buffer.Position;
            var str = _encoding.BaseEncoding.GetString(_buffer.Buffer, _buffer.Position, length);
            b.Append(str);
            _buffer.Advance(length);
        }

        public string ReadString()
        {
            var b = new StringBuilder();
            var offset = _buffer.Position;

            do {
                if ((_buffer.Length - offset) < _encoding.BinaryFormat.MaxSize) {
                    AppendString(b, offset);
                    _buffer.RequestSpace(_encoding.BinaryFormat.MaxSize);
                    offset = _buffer.Position;
                }
                if (IsNextCharacter(_encoding.Quote, offset)) {
                    AppendString(b, offset);
                    _buffer.Advance(_encoding.Quote.Length);
                    return b.ToString();
                }
                if (IsNextCharacter(_encoding.ReverseSolidus, offset)) {
                    AppendString(b, offset);
                    offset += _encoding.ReverseSolidus.Length;
                    if (IsNextCharacter(_encoding.Backspace, offset)) {
                        b.Append('\b');
                    }
                    else if (IsNextCharacter(_encoding.ReverseSolidus, offset)) {
                        b.Append('\\');
                    }
                    else if (IsNextCharacter(_encoding.Quote, offset)) {
                        b.Append('\"');
                    }
                    else if (IsNextCharacter(_encoding.CarriageReturn, offset)) {
                        b.Append('\r');
                    }
                    else if (IsNextCharacter(_encoding.Formfeed, offset)) {
                        b.Append('\f');
                    }
                    else if (IsNextCharacter(_encoding.HorizontalTab, offset)) {
                        b.Append('\t');
                    }
                    else if (IsNextCharacter(_encoding.Newline, offset)) {
                        b.Append('\n');
                    }
                    else if (IsNextCharacter(_encoding.Solidus, offset)) {
                        b.Append('/');
                    }
                    else {
                        throw UnexpectedJsonException.From("escaped character.", _buffer, _encoding);
                    }

                }
                offset += _encoding.GetCharacterSize(_buffer.Buffer, offset);
            } while (true);

        }
    }
}
