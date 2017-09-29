namespace Enigma.Serialization.Json
{
    public interface IEncodingBinaryFormat
    {
        int MaxSize { get; }
        int MinSize { get; }
        int SizeIncrement { get; }
        byte[] ExpandCodes { get; }
        int MarkerOffset { get; }
    }
}