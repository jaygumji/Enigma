using System;
using System.Reflection;
using Xunit;

namespace Enigma.Test
{
    public class DynamicActivatorTests
    {

        public class ActivationClass
        {
            public int Constructor { get; }
            public ActivationClass() { Constructor = 0; }
            public ActivationClass(int x, int y, string message) { Constructor = 1; }
        }

        [Fact]
        public void ActivateParameterLess()
        {
            var type = typeof(ActivationClass);
            var constructor = type.GetTypeInfo().GetConstructor(Type.EmptyTypes);
            var activator = new DynamicActivator(typeof(ActivationClass), constructor);
            var instance = activator.Activate() as ActivationClass;
            Assert.NotNull(instance);
            Assert.Equal(0, instance.Constructor);
        }

        [Fact]
        public void ActivateStandardTypeParameters()
        {
            var type = typeof(ActivationClass);
            var parameterTypes = new Type[] {
                typeof(int),
                typeof(int),
                typeof(string)
            };
            var constructor = type.GetTypeInfo().GetConstructor(parameterTypes);
            var activator = new DynamicActivator(typeof(ActivationClass), constructor);
            var instance = activator.Activate(1, 2, "Hello World") as ActivationClass;
            Assert.NotNull(instance);
            Assert.Equal(1, instance.Constructor);
        }

    }
}
