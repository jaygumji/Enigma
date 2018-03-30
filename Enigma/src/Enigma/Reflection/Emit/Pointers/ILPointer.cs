/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using System.Reflection;
using System.Reflection.Emit;
using Enigma.Reflection.Emit.Pointers;

namespace Enigma.Reflection.Emit
{
    public abstract class ILPointer : IILPointer
    {

        public abstract Type Type { get; }

        void IILPointer.Load(ILGenerator il)
        {
            Load(il);
        }

        void IILPointer.LoadAddress(ILGenerator il)
        {
            LoadAddress(il);
        }

        protected abstract void Load(ILGenerator il);

        protected virtual void LoadAddress(ILGenerator il)
        {
            throw new NotSupportedException("This parameter does not support address loading, " + GetType().Name);
        }

        public static ILVariable Of(LocalBuilder local)
        {
            return new ILLocalVariable(local);
        }

        public static ILPointer Of(ILCallMethodSnippet snippet)
        {
            return new ILDelegatedPointer(snippet.ReturnType, il => ((IILSnippet)snippet).Generate(il));
        }

        public static ILPointer Of(IILSnippet code)
        {
            return new ILDelegatedPointer(null, code.Generate);
        }

        public static ILPointer Of(string value)
        {
            return new ILStringConstantPointer(value);
        }

        public static ILPointer Of(int value)
        {
            return new ILInt32ConstantPointer(value);
        }

        public static ILPointer Of(uint value)
        {
            return new ILInt32ConstantPointer((int)value);
        }

        public static ILPointer Of(Type type)
        {
            return new ILTypePointer(type);
        }

        public static ILInstanceFieldVariable Field(ILPointer instance, FieldInfo field)
        {
            return new ILInstanceFieldVariable(instance, field);
        }

        public static ILInstanceFieldVariable Field(ILPointer instance, string fieldName)
        {
            var field = instance.Type.GetRuntimeField(fieldName)
                ?? throw new ArgumentException($"Could not find a field on {instance.Type} with name {fieldName}");

            return new ILInstanceFieldVariable(instance, field);
        }

        public static ILStaticFieldVariable Field(FieldInfo field)
        {
            return new ILStaticFieldVariable(field);
        }

        public static ILInstancePropertyVariable Property(ILPointer instance, PropertyInfo property)
        {
            return new ILInstancePropertyVariable(instance, property);
        }

        public static ILInstancePropertyVariable Property(ILPointer instance, string propertyName)
        {
            var property = instance.Type.GetRuntimeProperty(propertyName)
                ?? throw new ArgumentException($"Could not find a property on {instance.Type} with name {propertyName}");

            return new ILInstancePropertyVariable(instance, property);
        }

        public static ILArgPointer Arg(int index)
        {
            return Arg(index, typeof(object));
        }

        public static ILArgPointer Arg(int index, Type argType)
        {
            return new ILArgPointer(argType, index);
        }

        public static ILNullPointer Null => ILNullPointer.Instance;

        public static ILArgPointer This()
        {
            return ILArgPointer.This;
        }

        public static ILArgPointer This(Type type)
        {
            return Arg(0, type);
        }

        public static ILNewPointer New(ConstructorInfo constructor, params ILPointer[] args)
        {
            return new ILNewPointer(constructor, args);
        }

        public static implicit operator ILPointer(LocalBuilder local)
        {
            return Of(local);
        }

        public static implicit operator ILPointer(ILCallMethodSnippet ilCallMethod)
        {
            return Of(ilCallMethod);
        }

        public static implicit operator ILPointer(string value)
        {
            return Of(value);
        }

        public static implicit operator ILPointer(int value)
        {
            return Of(value);
        }

        public static implicit operator ILPointer(bool value)
        {
            return Of(value ? 1 : 0);
        }

        public static implicit operator ILPointer(uint value)
        {
            return Of(value);
        }

        public static implicit operator ILPointer(Type type)
        {
            return Of(type);
        }

    }
}