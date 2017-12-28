using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public abstract class ILConstantPointer : ILPointer
    {
        public override Type Type { get; }

        protected ILConstantPointer(Type type)
        {
            Type = type;
        }

        protected override void LoadAddress(ILGenerator il)
        {
            Load(il);
        }
    }
}