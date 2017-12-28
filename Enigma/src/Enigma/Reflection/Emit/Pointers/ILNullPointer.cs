using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public sealed class ILNullPointer : ILPointer
    {
        public static ILNullPointer Instance { get; } = new ILNullPointer();

        public override Type Type => null;

        private ILNullPointer() { }

        protected override void Load(ILGenerator il)
        {
            il.Emit(OpCodes.Ldnull);
        }
    }
}