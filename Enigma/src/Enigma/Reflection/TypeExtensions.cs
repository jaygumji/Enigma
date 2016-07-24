using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Enigma.Reflection
{
    public static class TypeExtensions
    {
        public static readonly Type CollectionType = typeof (ICollection<>);
        public static readonly Type DictionaryType = typeof (IDictionary<,>);
        public static readonly Type NullableType = typeof (Nullable<>);

        public static IContainerTypeInfo GetContainerTypeInfo(this Type type)
        {
            if (type.IsArray) {
                var ranks = type.GetArrayRank();
                var elementType = type.GetElementType();
                return new ArrayContainerTypeInfo(elementType, ranks);
            }

            var typeInfo = type.GetTypeInfo();
            if (type.GetTypeInfo().IsGenericType) {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == DictionaryType) {
                    var arguments = typeInfo.GetGenericArguments();
                    return new DictionaryContainerTypeInfo(arguments[0], arguments[1]);
                }

                if (genericTypeDefinition == CollectionType)
                    return new CollectionContainerTypeInfo(typeInfo.GetGenericArguments()[0]);

                if (genericTypeDefinition == NullableType)
                    return new NullableContainerTypeInfo(type, typeInfo.GetGenericArguments()[0]);
            }

            var interfaceTypes = typeInfo.GetInterfaces();
            foreach (var interfaceType in interfaceTypes.Where(interfaceType => interfaceType.GetTypeInfo().IsGenericType)) {
                var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();
                var interfaceTypeInfo = interfaceType.GetTypeInfo();
                if (genericTypeDefinition == CollectionType)
                    return new CollectionContainerTypeInfo(interfaceTypeInfo.GetGenericArguments()[0]);
                if (genericTypeDefinition == DictionaryType) {
                    var arguments = interfaceTypeInfo.GetGenericArguments();
                    return new DictionaryContainerTypeInfo(arguments[0], arguments[1]);
                }
            }

            return null;
        }

        public static Type AsNullable(this Type type)
        {
            return NullableType.MakeGenericType(type);
        }

    }
}