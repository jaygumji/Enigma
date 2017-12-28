using System.Reflection;

namespace Enigma.Test.Reflection.Emit
{
    public class EmitTestClass : IEmitTest
    {
        public static readonly ConstructorInfo ConstructorInfo
            = typeof(EmitTestClass).GetConstructor(new[] { typeof(string) });

        public string Message { get; }

        public EmitTestClass(string msg)
        {
            Message = msg;
        }
    }
}