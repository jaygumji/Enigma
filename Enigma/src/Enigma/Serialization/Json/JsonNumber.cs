namespace Enigma.Serialization.Json
{
    public struct JsonNumber : IJsonNode
    {
        public double? Value { get; }

        public JsonNumber(double? value)
        {
            Value = value;
        }

        public bool IsNull => !Value.HasValue;

        public override string ToString()
        {
            return Value.HasValue ? Value.ToString() : "null";
        }
    }
}