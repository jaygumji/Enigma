using System;
using System.Collections.Generic;
using System.Reflection;
using Enigma.Reflection;

namespace Enigma.Serialization.Reflection.Emit
{
    public class DictionaryMembers
    {
        public readonly Type VariableType;
        public readonly Type KeyType;
        public readonly Type ValueType;
        public readonly Type ElementType;
        public readonly MethodInfo Add;
        public readonly ConstructorInfo Constructor;

        public DictionaryMembers(WrappedType dictionaryType)
        {
            var container = dictionaryType.Container.AsDictionary();
            KeyType = container.KeyType;
            ValueType = container.ValueType;
            ElementType = container.ElementType;
            VariableType = typeof (IDictionary<,>).MakeGenericType(KeyType, ValueType);

            Add = VariableType.GetTypeInfo().GetMethod("Add", new[] {KeyType, ValueType});
            var instanceType = dictionaryType.Info.IsInterface
                ? typeof (Dictionary<,>).MakeGenericType(KeyType, ValueType)
                : dictionaryType.Ref;
            Constructor = instanceType.GetTypeInfo().GetConstructor(Type.EmptyTypes);
            if (Constructor == null) throw InvalidGraphException.NoParameterLessConstructor(dictionaryType.Ref);
        }

    }
}