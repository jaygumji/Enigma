using Enigma.IoC;
using Enigma.Test.IoC.Fakes;
using Enigma.Testing.Fakes.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Enigma.Test.IoC
{
    public class IoCContainerTests
    {

        [Fact]
        public void CreateParameterlessInstance()
        {
            var factory = new IoCFactory(new Dictionary<Type, IIoCRegistration>());
            for (var i = 0; i < 20; i++) {
                var instance = factory.GetInstance(typeof(DataBlock));
                Assert.NotNull(instance);
            }
        }

        [Fact]
        public void CreateDependentInstance()
        {
            var container = new IoCContainer();
            container.Register<ICoreCalculator, CoreCalculator>();
            container.Register<ICoreValidator>(new CoreValidator(4));
            container.Register(CoreConfig.Default);

            var core = container.GetInstance<Core>();
            core.Calculate(4, 6, 5);
        }

    }
}
