using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILInstanceFieldVariable : ILVariable
    {
        public ILPointer Instance { get; }
        public FieldInfo Info { get; }
        public override Type Type => Info.FieldType;

        public ILInstanceFieldVariable(ILPointer instance, FieldInfo info)
        {
            Instance = instance;
            Info = info;
        }

        protected override void Load(ILGenerator il)
        {
            if (Instance.Type.GetTypeInfo().IsValueType) {
                il.LoadAddress(Instance);
            }
            else {
                il.Load(Instance);
            }

            il.Emit(OpCodes.Ldfld, Info);
        }

        protected override void LoadAddress(ILGenerator il)
        {
            if (Instance.Type.GetTypeInfo().IsValueType) {
                il.LoadAddress(Instance);
            }
            else {
                il.Load(Instance);
            }

            il.Emit(OpCodes.Ldflda, Info);
        }

        protected override void OnPreSet(ILGenerator il)
        {
            if (Instance.Type.GetTypeInfo().IsValueType) {
                il.LoadAddress(Instance);
            }
            else {
                il.Load(Instance);
            }
        }

        protected override void OnSet(ILGenerator il)
        {
            il.Emit(OpCodes.Stfld, Info);
        }
    }
}