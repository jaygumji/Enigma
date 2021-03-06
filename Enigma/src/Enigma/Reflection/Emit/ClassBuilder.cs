﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
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

        public ClassBuilder(TypeBuilder typeBuilder)
        {
            _typeBuilder = typeBuilder;
            _isSealed = false;
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
            return new ConstructorBuilder(constructorBuilder);
        }

        public ConstructorBuilder DefineConstructor(params Type[] parameterTypes)
        {
            var constructorBuilder = _typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            return new ConstructorBuilder(constructorBuilder);
        }

        public MethodBuilder DefineMethod(string name, Type returnType, Type[] parameterTypes)
        {
            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig;
            var methodBuilder = _typeBuilder.DefineMethod(name, attributes, returnType, parameterTypes);
            return new MethodBuilder(methodBuilder);
        }

        public MethodBuilder DefineOverloadMethod(string name, Type returnType, Type[] parameterTypes)
        {
            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Final | MethodAttributes.NewSlot | MethodAttributes.Virtual;
            var methodBuilder = _typeBuilder.DefineMethod(name, attributes, returnType, parameterTypes);
            return new MethodBuilder(methodBuilder);
        }

        public void Seal()
        {
            if (_isSealed) return;
            _isSealed = true;

            _type = _typeBuilder.CreateTypeInfo().AsType();
        }

    }
}
