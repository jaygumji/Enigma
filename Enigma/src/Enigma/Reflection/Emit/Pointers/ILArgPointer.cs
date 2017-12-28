using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public sealed class ILArgPointer : ILPointer
    {
        private readonly int _argumentIndex;

        public static ILArgPointer This { get; } = new ILArgPointer(0);

        public override Type Type { get; }

        public ILArgPointer(int argumentIndex)
            : this(typeof(object), argumentIndex)
        {
        }

        public ILArgPointer(Type type, int argumentIndex)
        {
            _argumentIndex = argumentIndex;
            Type = type;
        }

        protected override void Load(ILGenerator il)
        {
            if (_argumentIndex == 0) {
                il.Emit(OpCodes.Ldarg_0);
            }
            else if (_argumentIndex == 1) {
                il.Emit(OpCodes.Ldarg_1);
            }
            else if (_argumentIndex == 2) {
                il.Emit(OpCodes.Ldarg_2);
            }
            else if (_argumentIndex == 3) {
                il.Emit(OpCodes.Ldarg_3);
            }
            else {
                il.Emit(OpCodes.Ldarg, _argumentIndex);
            }
        }

        protected override void LoadAddress(ILGenerator il)
        {
            il.Emit(OpCodes.Ldarga, _argumentIndex);
        }
    }
}