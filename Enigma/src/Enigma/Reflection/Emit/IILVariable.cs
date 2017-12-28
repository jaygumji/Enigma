using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public interface IILVariable : IILPointer
    {
        void PreSet(ILGenerator il);
        void Set(ILGenerator il);
    }
}