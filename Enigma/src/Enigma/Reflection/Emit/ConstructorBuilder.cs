using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public class ConstructorBuilder
    {
        private readonly System.Reflection.Emit.ConstructorBuilder _builder;

        public ConstructorBuilder(System.Reflection.Emit.ConstructorBuilder builder)
        {
            _builder = builder;
            IL = builder.GetILGenerator();
        }

        public ILGenerator IL { get; }
        public ConstructorInfo Reference => _builder;
    }
}