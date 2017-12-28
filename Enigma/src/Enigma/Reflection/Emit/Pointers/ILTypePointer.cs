using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILTypePointer : ILPointer
    {

        private static readonly MethodInfo MethodGetTypeFromHandleToken =
            typeof(Type).GetTypeInfo().GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) });

        private readonly Type _type;
        public override Type Type => typeof(Type);

        public ILTypePointer(Type type)
        {
            _type = type;
        }

        protected override void Load(ILGenerator il)
        {
            il.Emit(OpCodes.Ldtoken, _type);
            il.EmitCall(OpCodes.Call, MethodGetTypeFromHandleToken, null);
        }

        protected override void LoadAddress(ILGenerator il)
        {
            Load(il);
        }
    }
}