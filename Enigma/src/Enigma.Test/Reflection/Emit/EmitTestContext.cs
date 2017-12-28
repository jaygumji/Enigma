using System.Reflection;

namespace Enigma.Test.Reflection.Emit
{
    public class EmitTestContext
    {
        public static readonly MethodInfo GetTestTextMethodInfo = typeof(EmitTestContext).GetMethod("GetTestText");

        public static string GetTestText()
        {
            return "Hello World";
        }
    }
}