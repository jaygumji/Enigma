namespace Enigma.Binary.Converters
{
    public interface IBase64CharacterMap
    {
        unsafe void MapLast(ref byte* s, ref byte* t, ref int padding);
        unsafe void MapTo(ref byte* s, ref byte* t);
    }
}