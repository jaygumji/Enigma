using System;

namespace Enigma.Reflection
{
    public class CollectionContainerTypeInfo : IContainerTypeInfo
    {
        private readonly Lazy<Type> _collectionInterfaceType;
        public Type ElementType { get; }

        public CollectionContainerTypeInfo(Type elementType)
        {
            ElementType = elementType;
            _collectionInterfaceType = new Lazy<Type>(() => TypeExtensions.CollectionType.MakeGenericType(elementType));
        }

        public Type CollectionInterfaceType => _collectionInterfaceType.Value;
    }
}