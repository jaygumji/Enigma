using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{
    public interface IILSnippet
    {
        void Generate(ILGenerator il);
    }

    public abstract class ILSnippet : IILSnippet
    {
        void IILSnippet.Generate(ILGenerator il)
        {
            OnGenerate(il);
        }

        protected abstract void OnGenerate(ILGenerator il);

        public static ILCallMethodSnippet Call(MethodInfo method, params ILPointer[] parameters)
        {
            return new ILCallMethodSnippet(method, parameters);
        }

        public static ILCallMethodSnippet Call(ILPointer instance, MethodInfo method, params ILPointer[] parameters)
        {
            return new ILCallMethodSnippet(instance, method, parameters);
        }

        public static ILCallMethodSnippet Call(ILPointer instance, string methodName, params ILPointer[] parameters)
        {
            if (instance == null) {
                throw new ArgumentNullException(nameof(instance));
            }
            if (instance.Type == null) {
                throw new ArgumentException("The instance type is missing.");
            }
            var method = instance.Type.GetTypeInfo().GetMethod(methodName);
            if (method == null) {
                throw new ArgumentException($"The method {methodName} could not be found on type {instance.Type.FullName}");
            }
            return new ILCallMethodSnippet(instance, method, parameters);
        }
    }
}
