namespace Enigma.Serialization.Json
{
    public struct JsonBool : IJsonNode
    {
        public static IJsonNode True { get; } = new JsonBool(true);
        public static IJsonNode False { get; } = new JsonBool(false);

        public bool? Value { get; }

        public JsonBool(bool? value)
        {
            Value = value;
        }

        public bool IsNull => !Value.HasValue;

        public override string ToString()
        {
            return Value.HasValue
                ? Value.Value ? "true" : "false"
                : "null";
        }
    }
}