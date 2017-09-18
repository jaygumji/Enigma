using System;
using System.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class AcquirePropertyMetadataArgs
    {
        public Type Type { get; }

        public PropertyInfo Property { get; }

        public uint? Index { get; set; }

        public AcquirePropertyMetadataArgs(Type type, PropertyInfo property)
        {
            Type = type;
            Property = property;
        }

    }
}