using System;
using Enigma.Binary;

namespace Enigma.Serialization.Json
{
    public class UnexpectedJsonException : Exception
    {

        public UnexpectedJsonException(string message)
            : base(message)
        {
        }

        public static UnexpectedJsonException From(string expected, BinaryReadBuffer buffer, JsonEncoding encoding)
        {
            return new UnexpectedJsonException("Unexpected token in json. Expected " + expected);
        }

        public static UnexpectedJsonException InObject(JsonLiteral literal)
        {
            return new UnexpectedJsonException("An object can not directly contain " + literal);
        }

        public static UnexpectedJsonException InArray(JsonLiteral literal)
        {
            return new UnexpectedJsonException("An array can not directly contain " + literal);
        }

        public static UnexpectedJsonException Type(string name, IJsonNode node, Type expectedType)
        {
            return new UnexpectedJsonException($"Unable to parse field {name}, expected {expectedType.Name}, but found {node.GetType().Name}");
        }
    }
}