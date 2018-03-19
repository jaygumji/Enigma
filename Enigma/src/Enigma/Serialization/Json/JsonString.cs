namespace Enigma.Serialization.Json
{
    public struct JsonString : IJsonNode
    {
        public string Value { get; }

        public JsonString(string value)
        {
            Value = value;
        }

        public bool IsNull => Value == null;

        public override string ToString()
        {
            return Value;
        }
    }
}