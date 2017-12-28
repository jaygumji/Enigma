using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit.Pointers
{
    public class ILEqualPointer : ILPointer
    {
        private readonly IILPointer _left;
        private readonly IILPointer _right;
        public override Type Type => typeof(bool);

        public ILEqualPointer(IILPointer left, IILPointer right)
        {
            _left = left;
            _right = right;
        }

        protected override void Load(ILGenerator il)
        {
            _left.Load(il);
            _right.Load(il);
            il.Emit(OpCodes.Ceq);
        }
    }
}
