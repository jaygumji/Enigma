using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILInstancePropertyVariable : ILVariable
    {
        public ILPointer Instance { get; }
        public PropertyInfo Info { get; }
        public override Type Type => Info.PropertyType;

        public ILInstancePropertyVariable(ILPointer instance, PropertyInfo info)
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

            if (Instance.Type.GetTypeInfo().IsValueType) {
                il.EmitCall(OpCodes.Call, Info.GetMethod, null);
            }
            else {
                il.EmitCall(OpCodes.Callvirt, Info.GetMethod, null);
            }
        }

        protected override void LoadAddress(ILGenerator il)
        {
            Load(il);
            var local = il.NewLocal(Info.PropertyType);
            il.Set(local);
            il.LoadAddress(local);
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
            if (Type.GetTypeInfo().IsValueType) {
                il.EmitCall(OpCodes.Call, Info.SetMethod, null);
            }
            else {
                il.EmitCall(OpCodes.Callvirt, Info.SetMethod, null);
            }
        }

    }
}