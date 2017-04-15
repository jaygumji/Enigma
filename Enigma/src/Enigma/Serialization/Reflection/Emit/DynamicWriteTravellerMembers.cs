using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Enigma.Reflection;
using Enigma.Reflection.Emit;

namespace Enigma.Serialization.Reflection.Emit
{
    public sealed class DynamicWriteTravellerMembers
    {

        public readonly ILCodeParameter VisitArgsCollectionItem;
        public readonly ILCodeParameter VisitArgsDictionaryKey;
        public readonly ILCodeParameter VisitArgsDictionaryValue;
        public readonly ILCodeParameter VisitArgsCollectionInCollection;
        public readonly ILCodeParameter VisitArgsDictionaryInCollection;
        public readonly ILCodeParameter VisitArgsDictionaryInDictionaryKey;
        public readonly ILCodeParameter VisitArgsDictionaryInDictionaryValue;
        public readonly ILCodeParameter VisitArgsCollectionInDictionaryKey;
        public readonly ILCodeParameter VisitArgsCollectionInDictionaryValue;

        public readonly MethodInfo VisitorVisit;
        public readonly MethodInfo VisitorLeave;
        public readonly Dictionary<Type, MethodInfo> VisitorVisitValue;

        public readonly Dictionary<Type, ConstructorInfo> NullableConstructors; 

        public readonly MethodInfo EnumeratorMoveNext;
        public readonly MethodInfo DisposableDispose;

        public readonly MethodInfo ArrayGetLength;

        public DynamicWriteTravellerMembers(ITypeProvider provider)
        {
            var visitArgsType = typeof (VisitArgs);
            var visitArgsTypeInfo = visitArgsType.GetTypeInfo();
            VisitArgsCollectionItem = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("CollectionItem"));
            VisitArgsDictionaryKey = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("DictionaryKey"));
            VisitArgsDictionaryValue = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("DictionaryValue"));
            VisitArgsCollectionInCollection = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("CollectionInCollection"));
            VisitArgsDictionaryInCollection = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("DictionaryInCollection"));
            VisitArgsDictionaryInDictionaryKey = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("DictionaryInDictionaryKey"));
            VisitArgsDictionaryInDictionaryValue = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("DictionaryInDictionaryValue"));
            VisitArgsCollectionInDictionaryKey = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("CollectionInDictionaryKey"));
            VisitArgsCollectionInDictionaryValue = new StaticFieldILCodeVariable(visitArgsTypeInfo.GetField("CollectionInDictionaryValue"));

            var writeVisitorType = typeof (IWriteVisitor);
            var writeVisitorTypeInfo = writeVisitorType.GetTypeInfo();
            VisitorVisit = writeVisitorTypeInfo.GetMethod("Visit");
            VisitorLeave = writeVisitorTypeInfo.GetMethod("Leave");

            VisitorVisitValue = new Dictionary<Type, MethodInfo>();
            NullableConstructors = new Dictionary<Type, ConstructorInfo>();
            var nullableType = typeof (Nullable<>);
            foreach (var method in writeVisitorTypeInfo.GetMethods()
                .Where(m => m.Name == "VisitValue")) {

                var valueType = method.GetParameters()[0].ParameterType;
                var valueTypeExt = provider.Extend(valueType);

                VisitorVisitValue.Add(valueType, method);
                if (valueTypeExt.Class == TypeClass.Nullable) {
                    var innerType = valueTypeExt.Container.AsNullable().ElementType;
                    VisitorVisitValue.Add(innerType, method);

                    var nullableTypeInfo = nullableType.MakeGenericType(innerType).GetTypeInfo();
                    NullableConstructors.Add(innerType, nullableTypeInfo.GetConstructor(new []{innerType}));
                }
            }

            EnumeratorMoveNext = typeof (IEnumerator).GetTypeInfo().GetMethod("MoveNext");
            DisposableDispose = typeof (IDisposable).GetTypeInfo().GetMethod("Dispose");
            ArrayGetLength = typeof (Array).GetTypeInfo().GetMethod("GetLength");
        }

    }
}