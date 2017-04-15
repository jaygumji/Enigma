using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ClassBuilder
    {
        private readonly TypeBuilder _typeBuilder;
        private Type _type;
        private bool _isSealed;
        private readonly ITypeProvider _provider;

        public ClassBuilder(TypeBuilder typeBuilder, ITypeProvider provider)
        {
            _typeBuilder = typeBuilder;
            _isSealed = false;
            _provider = provider;
        }

        // TODO: Check _typeBuilder.AsType call
        public Type Type { get { return _isSealed ? _type : _typeBuilder.AsType(); } }
        public bool IsSealed { get { return _isSealed; } }

        public FieldInfo DefinePrivateField(string fieldName, Type fieldType)
        {
            return _typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Private);
        }

        public ConstructorBuilder DefineDefaultConstructor()
        {
            var constructorBuilder = _typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            return new ConstructorBuilder(constructorBuilder, _provider);
        }

        public ConstructorBuilder DefineConstructor(params Type[] parameterTypes)
        {
            var constructorBuilder = _typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            return new ConstructorBuilder(constructorBuilder, _provider);
        }

        public MethodBuilder DefineMethod(string name, Type returnType, Type[] parameterTypes)
        {
            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig;
            var methodBuilder = _typeBuilder.DefineMethod(name, attributes, returnType, parameterTypes);
            return new MethodBuilder(methodBuilder, _provider);
        }

        public MethodBuilder DefineOverloadMethod(string name, Type returnType, Type[] parameterTypes)
        {
            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Final | MethodAttributes.NewSlot | MethodAttributes.Virtual;
            var methodBuilder = _typeBuilder.DefineMethod(name, attributes, returnType, parameterTypes);
            return new MethodBuilder(methodBuilder, _provider);
        }

        public void Seal()
        {
            if (_isSealed) return;
            _isSealed = true;

            _type = _typeBuilder.CreateTypeInfo().AsType();
        }

    }
}
