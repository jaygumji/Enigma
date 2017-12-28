using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit.Pointers
{
    public class ILNegatePointer : ILPointer
    {
        private readonly IILPointer _value;

        public override Type Type => typeof(bool);

        public ILNegatePointer(IILPointer value)
        {
            _value = value;
        }

        protected override void Load(ILGenerator il)
        {
            _value.Load(il);
            il.Load(0);
            il.Emit(OpCodes.Ceq);
        }
    }
}
