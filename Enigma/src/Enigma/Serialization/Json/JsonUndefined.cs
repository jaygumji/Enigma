namespace Enigma.Serialization.Json
{
    public class JsonUndefined : IJsonNode
    {

        public static JsonUndefined Instance { get; } = new JsonUndefined();

        private JsonUndefined()
        {

        }

        public bool IsNull => true;

        public override string ToString()
        {
            return "undefined";
        }
    }
}