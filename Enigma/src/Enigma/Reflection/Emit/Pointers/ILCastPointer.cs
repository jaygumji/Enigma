using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILCastPointer : ILPointer
    {
        private readonly IILPointer _pointer;
        private readonly Type _toType;

        public ILCastPointer(IILPointer pointer, Type toType)
        {
            _pointer = pointer;
            _toType = toType;
        }

        public override Type Type => _toType;

        protected override void Load(ILGenerator il)
        {
            _pointer.Load(il);
            if (_pointer.Type != _toType) {
                il.Emit(OpCodes.Castclass, _toType);
            }
        }

    }
}