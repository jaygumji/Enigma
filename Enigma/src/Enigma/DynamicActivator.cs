using Enigma.Reflection;
using Enigma.Reflection.Emit;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma
{
    public class DynamicActivator
    {

        private static readonly Type[] DynConstructorTypes = new[] { typeof(object[]) };

        private readonly Func<object[], object> _activate;

        public Type Type { get; }

        public DynamicActivator(Type type, ConstructorInfo constructor)
            : this(type, constructor, new FactoryTypeProvider())
        {
        }

        public DynamicActivator(Type type, ConstructorInfo constructor, ITypeProvider provider)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            if (constructor == null) throw new ArgumentNullException(nameof(constructor));
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var methodName = "D$Activator$" + type.FullName.Replace(".", "_");
            var method = new DynamicMethod(methodName, typeof(object), DynConstructorTypes);

            var il = method.GetILGenerator();
            var gen = new ILExpressed(il, provider);
            var methodParams = gen.Var.Arg(0, typeof(object[]));

            var parameters = constructor.GetParameters();

            var constructParameters = new ILCodeParameter[parameters.Length];
            for (var i = 0; i < parameters.Length; i++) {
                var parameter = parameters[i];
                var constructParam = gen.Var.ArrayElement(methodParams, i)
                    .Cast(parameter.ParameterType);

                constructParameters[i] = constructParam;
            }

            gen.Snippets.ConstructInstance(constructor, constructParameters);
            gen.Return();

            _activate = (Func<object[], object>)
                method.CreateDelegate(typeof(Func<object[], object>));
        }

        public object Activate(params object[] parameters)
        {
            return _activate.Invoke(parameters);
        }

    }
}
