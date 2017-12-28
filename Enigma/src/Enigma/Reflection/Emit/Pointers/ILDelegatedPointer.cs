using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILDelegatedPointer : ILPointer
    {
        private readonly ILGenerationMethodHandler _valueLoader;
        private readonly ILGenerationMethodHandler _valueAddressLoader;

        public override Type Type { get; }

        public ILDelegatedPointer(Type parameterType, ILGenerationMethodHandler valueLoader) : this(parameterType, valueLoader, null)
        {
        }

        public ILDelegatedPointer(Type parameterType, ILGenerationMethodHandler valueLoader, ILGenerationMethodHandler valueAddressLoader)
        {
            Type = parameterType;
            _valueLoader = valueLoader;
            _valueAddressLoader = valueAddressLoader;
        }

        protected override void Load(ILGenerator il)
        {
            _valueLoader.Invoke(il);
        }

        protected override void LoadAddress(ILGenerator il)
        {
            if (_valueAddressLoader == null)
                throw new NotSupportedException("This parameter does not support address loading");

            _valueAddressLoader.Invoke(il);
        }
    }
}