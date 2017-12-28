using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class MethodBuilder
    {

        private readonly System.Reflection.Emit.MethodBuilder _methodBuilder;

        public MethodBuilder(System.Reflection.Emit.MethodBuilder methodBuilder)
        {
            _methodBuilder = methodBuilder;
            IL = _methodBuilder.GetILGenerator();
        }

        public MethodInfo Method => _methodBuilder;
        public ILGenerator IL { get; }
    }
}