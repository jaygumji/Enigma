using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILStaticFieldVariable : ILVariable
    {
        public FieldInfo Info { get; }
        public override Type Type => Info.FieldType;

        public ILStaticFieldVariable(FieldInfo info)
        {
            Info = info;
        }

        protected override void Load(ILGenerator il)
        {
            il.Emit(OpCodes.Ldsfld, Info);
        }

        protected override void LoadAddress(ILGenerator il)
        {
            il.Emit(OpCodes.Ldsflda, Info);
        }

        protected override void OnSet(ILGenerator il)
        {
            il.Emit(OpCodes.Stsfld, Info);
        }

    }
}