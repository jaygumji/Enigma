using System;
using System.Collections.Generic;

namespace Enigma.Reflection
{
    public class DictionaryContainerTypeInfo : CollectionContainerTypeInfo
    {

        private static readonly Type KeyValuePairType = typeof(KeyValuePair<,>);

        private readonly Lazy<Type> _dictionaryInterfaceType;

        public DictionaryContainerTypeInfo(Type keyType, Type valueType, ITypeProvider provider)
            : base(KeyValuePairType.MakeGenericType(keyType, valueType), provider)
        {
            KeyType = keyType;
            ValueType = valueType;

            _dictionaryInterfaceType = new Lazy<Type>(() => TypeExtensions.DictionaryType.MakeGenericType(keyType, valueType));
        }

        public Type KeyType { get; }
        public Type ValueType { get; }

        public ExtendedType KeyTypeExt => Provider.Extend(KeyType);
        public ExtendedType ValueTypeExt => Provider.Extend(ValueType);

        public Type DictionaryInterfaceType { get { return _dictionaryInterfaceType.Value; } }
    }
}