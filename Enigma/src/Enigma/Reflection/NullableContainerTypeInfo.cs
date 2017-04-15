using System;
using System.Reflection;

namespace Enigma.Reflection
{
    public class NullableContainerTypeInfo : IContainerTypeInfo
    {
        private readonly Type _elementType;
        private readonly Lazy<ConstructorInfo> _constructor;
        private readonly Lazy<MethodInfo> _getHasValueMethod;
        private readonly ITypeProvider _provider;

        public NullableContainerTypeInfo(Type type, Type elementType, ITypeProvider provider)
        {
            _elementType = elementType;
            var typeInfo = type.GetTypeInfo();
            _constructor = new Lazy<ConstructorInfo>(() => typeInfo.GetConstructor(new[] {elementType}));
            _getHasValueMethod = new Lazy<MethodInfo>(() => typeInfo.GetProperty("HasValue").GetGetMethod());
            _provider = provider;
        }

        public Type ElementType { get { return _elementType; } }
        public ExtendedType ElementTypeExt => _provider.Extend(_elementType);
        public ConstructorInfo Constructor { get { return _constructor.Value; } }
        public MethodInfo GetHasValueMethod { get { return _getHasValueMethod.Value; } }
    }
}