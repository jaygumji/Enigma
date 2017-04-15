using Enigma.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Enigma.Serialization.Reflection
{
    public class SerializableTypeProvider
    {
        private const BindingFlags PropertyFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly SerializationReflectionInspector _inspector;
        private readonly Dictionary<Type, SerializableType> _types;
        public ITypeProvider Provider { get; }

        public SerializableTypeProvider(SerializationReflectionInspector inspector, ITypeProvider provider)
        {
            _inspector = inspector;
            _types = new Dictionary<Type, SerializableType>();
            Provider = provider;
        }

        public SerializableType GetOrCreate(Type type)
        {
            SerializableType serializableType;
            if (_types.TryGetValue(type, out serializableType))
                return serializableType;

            serializableType = Build(type);
            _types.Add(type, serializableType);
            return serializableType;
        }

        private SerializableType Build(Type type)
        {
            var properties = type.GetTypeInfo().GetProperties(PropertyFlags);

            UInt32 nextIndex = 1;

            var serializableProperties = new Dictionary<string, SerializableProperty>();
            foreach (var property in properties) {
                if (!_inspector.CanBeSerialized(type, property)) continue;
                if (serializableProperties.ContainsKey(property.Name))
                    throw InvalidGraphException.DuplicateProperties(type, property);

                var metadata = _inspector.AcquirePropertyMetadata(type, property, ref nextIndex);
                
                var ser = new SerializableProperty(property, metadata, Provider);
                serializableProperties.Add(property.Name, ser);
            }
            return new SerializableType(type, serializableProperties);
        }
    }
}