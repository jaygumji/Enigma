using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public delegate void ILGenerationHandler();
    public delegate void ILGenerationMethodHandler(ILGenerator il);

    public delegate void ILGenerationHandler<in T>(T value) where T : ILPointer;
    public delegate void ILGenerationMethodHandler<in T>(ILGenerator il, T value) where T : ILPointer;
}