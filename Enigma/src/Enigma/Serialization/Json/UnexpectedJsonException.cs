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

    }
}