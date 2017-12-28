using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILLocalVariable : ILVariable
    {
        public LocalBuilder Local { get; }

        public override Type Type => Local.LocalType;

        public ILLocalVariable(LocalBuilder local)
        {
            Local = local;
        }

        protected override void Load(ILGenerator il)
        {
            if (Local.LocalIndex == 0) {
                il.Emit(OpCodes.Ldloc_0);
            }
            else if (Local.LocalIndex == 1) {
                il.Emit(OpCodes.Ldloc_1);
            }
            else if (Local.LocalIndex == 2) {
                il.Emit(OpCodes.Ldloc_2);
            }
            else if (Local.LocalIndex == 3) {
                il.Emit(OpCodes.Ldloc_3);
            }
            else if (Local.LocalIndex <= byte.MaxValue) {
                il.Emit(OpCodes.Ldloc_S, Local.LocalIndex);
            }
            else {
                il.Emit(OpCodes.Ldloc, Local.LocalIndex);
            }
        }

        protected override void LoadAddress(ILGenerator il)
        {
            if (Local.LocalIndex <= byte.MaxValue) {
                il.Emit(OpCodes.Ldloca_S, Local.LocalIndex);
            }
            else {
                il.Emit(OpCodes.Ldloca, Local.LocalIndex);
            }
        }

        protected override void OnSet(ILGenerator il)
        {
            if (Local.LocalIndex == 0) {
                il.Emit(OpCodes.Stloc_0);
            }
            else if (Local.LocalIndex == 1) {
                il.Emit(OpCodes.Stloc_1);
            }
            else if (Local.LocalIndex == 2) {
                il.Emit(OpCodes.Stloc_2);
            }
            else if (Local.LocalIndex == 3) {
                il.Emit(OpCodes.Stloc_3);
            }
            else if (Local.LocalIndex <= byte.MaxValue) {
                il.Emit(OpCodes.Stloc_S, Local.LocalIndex);
            }
            else {
                il.Emit(OpCodes.Stloc, Local.LocalIndex);
            }
        }

        public static implicit operator ILLocalVariable(LocalBuilder local)
        {
            return new ILLocalVariable(local);
        }
    }
}