using System;

namespace Enigma.Reflection
{
    public class CollectionContainerTypeInfo : IContainerTypeInfo
    {
        private readonly Lazy<Type> _collectionInterfaceType;

        public CollectionContainerTypeInfo(Type elementType, ITypeProvider provider)
        {
            ElementType = elementType;
            _collectionInterfaceType = new Lazy<Type>(() => TypeExtensions.CollectionType.MakeGenericType(elementType));
            Provider = provider;
        }

        protected ITypeProvider Provider { get; }

        public Type ElementType { get; }
        public ExtendedType ElementTypeExt => Provider.Extend(ElementType);
        public Type CollectionInterfaceType => _collectionInterfaceType.Value;
    }
}