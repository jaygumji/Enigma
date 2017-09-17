using System.Reflection;
using Enigma.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class SerializableProperty
    {
        public SerializableProperty(PropertyInfo @ref, SerializationMetadata metadata, ITypeProvider provider)
        {
            Ref = @ref;
            Metadata = metadata;
            Ext = provider.Extend(Ref.PropertyType);
        }

        public PropertyInfo Ref { get; }

        public SerializationMetadata Metadata { get; }

        public ExtendedType Ext { get; }

    }
}