using System;

namespace Enigma.Binary.Converters
{
    public sealed class Base64BufferDecoder
    {

        private readonly byte[] _paddingChar;
        private readonly int _charSize;
        private readonly IBase64CharacterMap _map;

        public Base64BufferDecoder(Base64EncodedCharacterSet charSet)
        {
            _paddingChar = charSet.PaddingChar;
            _charSize = charSet.CharSize;
            _map = Base64CharacterMap.Create(charSet);
        }

        public int GetSizeOf(int count)
        {
            var blockCount = (count - 1) / 3 + 1;
            var numberOfChars = blockCount * 4;

            return numberOfChars * _charSize;
        }

        private unsafe bool IsEqual(byte* left, byte[] right)
        {
            for (var i = 0; i < _charSize; i++) {
                if (left[i] != right[i]) {
                    return false;
                }
            }
            return true;
        }

        public void Decode(byte[] source, int sourceOffset, int sourceCount, byte[] target, int targetOffset)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (sourceCount == 0) {
                return;
            }

            if (sourceOffset < 0 || sourceOffset >= source.Length) {
                throw new ArgumentException("The source offset is not within the bounds of the array.");
            }

            var charCount = sourceCount / _charSize;
            var blockCount = (charCount - 1) / 4 + 1;
            var numberOfBytes = blockCount * 3;
            var padding = blockCount * 4 - charCount;

            var targetLength = target.Length;

            if (numberOfBytes > targetLength - targetOffset) {
                throw new ArgumentException("The base64 encoding does not fit into the target array.");
            }

            unsafe {
                fixed (byte* startOfSource = source) {
                    var s = startOfSource;

                    if (_charSize == 1) {
                        if (sourceCount > 2 && s[sourceCount - 2] == _paddingChar[0]) {
                            padding = 2;
                        }
                        else if (sourceCount > 1 && s[sourceCount - 1] == _paddingChar[0]) {
                            padding = 1;
                        }
                    }
                    else {
                        if (sourceCount > 2 && IsEqual(s + sourceCount - (2 * _charSize), _paddingChar)) {
                            padding = 2;
                        }
                        else if (sourceCount > 1 && IsEqual(s + sourceCount - _charSize, _paddingChar)) {
                            padding = 1;
                        }
                    }

                    fixed (byte* startOfTarget = target) {
                        var t = startOfTarget;

                        for (var i = 1; i < blockCount; i++) {
                            _map.MapTo(ref s, ref t);
                        }

                        _map.MapLast(ref s, ref t, ref padding);
                    }
                }
            }
        }

    }
}