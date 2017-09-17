namespace Enigma.Serialization.Reflection
{
    public class SerializationMetadata
    {
        public static readonly SerializationMetadata Root = new SerializationMetadata(1, new StateBag());
        public static readonly SerializationMetadata Item = new SerializationMetadata(0, new StateBag());

        public SerializationMetadata(uint index, StateBag args)
        {
            Index = index;
            Args = args;
        }

        public uint Index { get; }
        public StateBag Args { get; }
    }
}