using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit.Pointers
{
    public class ILAddPointer : ILPointer
    {
        private readonly IILPointer _left;
        private readonly IILPointer _right;

        public override Type Type => _left.Type;

        public ILAddPointer(IILPointer left, IILPointer right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            if (left.Type != right.Type) {
                throw new ArgumentException("The types between the values doesn't match");
            }
        }

        protected override void Load(ILGenerator il)
        {
            _left.Load(il);
            _right.Load(il);
            il.Emit(OpCodes.Add);
        }
    }
}
