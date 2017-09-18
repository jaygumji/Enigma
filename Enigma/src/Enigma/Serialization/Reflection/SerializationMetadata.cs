namespace Enigma.Serialization.Reflection
{
    public class SerializationMetadata
    {
        public SerializationMetadata(uint index)
        {
            Index = index;
        }

        public uint Index { get; }
    }
}