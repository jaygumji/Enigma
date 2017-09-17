using System;

namespace Enigma.Serialization.Reflection
{
    public class TypeValidArgs
    {
        public Type Type { get; }
        public bool IsValid { get; set; }

        public TypeValidArgs(Type type)
        {
            Type = type;
            IsValid = true;
        }

    }
}