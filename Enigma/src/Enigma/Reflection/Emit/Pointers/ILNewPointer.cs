﻿using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit.Pointers
{
    public class ILNewPointer : ILPointer
    {
        private readonly ILPointer[] _constructorArguments;
        private readonly ConstructorInfo _constructor;
        public override Type Type => _constructor.DeclaringType;

        public ILNewPointer(ConstructorInfo constructor, params ILPointer[] constructorArguments)
        {
            _constructorArguments = constructorArguments;
            _constructor = constructor
                ?? throw new ArgumentNullException(nameof(constructor));

            if (constructorArguments.Length != constructor.GetParameters().Length) {
                throw new ArgumentException("The constructor parameter length does not match the specified arguments");
            }
        }

        protected override void Load(ILGenerator il)
        {
            foreach (var arg in _constructorArguments) {
                il.Load(arg);
            }
            il.Emit(OpCodes.Newobj, _constructor);
        }
    }
}
