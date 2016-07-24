using System;
using System.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public sealed class NullableMembers
    {
        public static readonly Type NullableTypeDefinition = typeof(Nullable<>);

        public readonly Type NullableType;
        public readonly ConstructorInfo Constructor;
        public readonly MethodInfo GetHasValue;
        public readonly MethodInfo GetValue;

        public NullableMembers(Type elementType)
        {
            NullableType = NullableTypeDefinition.MakeGenericType(elementType);

            var typeInfo = NullableType.GetTypeInfo();
            Constructor = typeInfo.GetConstructor(new[] { elementType });
            GetHasValue = typeInfo.GetProperty("HasValue").GetGetMethod();
            GetValue = typeInfo.GetProperty("Value").GetGetMethod();
        }
    }
}