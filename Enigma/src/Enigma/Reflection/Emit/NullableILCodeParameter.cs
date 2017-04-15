﻿using System;
using System.Reflection;

namespace Enigma.Reflection.Emit
{
    public class NullableILCodeParameter : ILCodeParameter
    {

        private readonly IILCodeParameter _parameter;

        public NullableILCodeParameter(IILCodeParameter parameter)
        {
            if (parameter.ParameterType == null) throw new NotSupportedException("The parameter does not have a well defined parameter type");

            _parameter = parameter;
        }

        public override Type ParameterType
        {
            get { return _parameter.ParameterType; }
        }

        protected override void Load(ILExpressed il)
        {
            _parameter.Load(il);

            var type = _parameter.ParameterType;

            if (!type.GetTypeInfo().IsValueType) return;

            var ext = il.Provider.Extend(type);
            if (ext.Class == TypeClass.Nullable) return;

            il.Snippets.AsNullable(type);
        }

    }
}