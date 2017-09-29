namespace Enigma.Serialization.Json
{
    public class EncodingBinaryFormat : IEncodingBinaryFormat
    {
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public int SizeIncrement { get; set; }
        public byte[] ExpandCodes { get; set; }
        public int MarkerOffset { get; set; }
    }
}