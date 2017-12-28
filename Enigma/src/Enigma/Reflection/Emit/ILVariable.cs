using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public abstract class ILVariable : ILPointer, IILVariable
    {

        void IILVariable.PreSet(ILGenerator il)
        {
            OnPreSet(il);
        }

        void IILVariable.Set(ILGenerator il)
        {
            OnSet(il);
        }

        protected virtual void OnPreSet(ILGenerator il)
        {
        }

        protected virtual void OnSet(ILGenerator il)
        {
            throw new InvalidOperationException("Not possible to use this variable in a set operation, " + GetType().Name);
        }

        public static implicit operator ILVariable(LocalBuilder local)
        {
            return new ILLocalVariable(local);
        }

    }
}