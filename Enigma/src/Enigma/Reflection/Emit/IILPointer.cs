using System;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public interface IILPointer
    {
        Type Type { get; }

        void Load(ILGenerator il);
        void LoadAddress(ILGenerator il);
    }
}