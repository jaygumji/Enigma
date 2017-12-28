using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ILStringConstantPointer : ILConstantPointer
    {
        private readonly string _value;

        public ILStringConstantPointer(string value) : base(typeof(string))
        {
            _value = value;
        }

        protected override void Load(ILGenerator il)
        {
            il.Emit(OpCodes.Ldstr, _value);
        }

    }
}