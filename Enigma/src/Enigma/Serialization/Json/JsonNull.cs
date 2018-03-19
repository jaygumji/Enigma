namespace Enigma.Serialization.Json
{
    public class JsonNull : IJsonNode
    {

        public static JsonNull Instance { get; } = new JsonNull();

        private JsonNull()
        {
            
        }

        public bool IsNull => true;

        public override string ToString()
        {
            return "null";
        }
    }
}