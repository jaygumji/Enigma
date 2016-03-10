using System.IO;
using System.Reflection;
using System.Resources;

namespace Enigma.Test
{
    public static class Resource
    {
        public static Stream Get(string name)
        {
            return typeof(Resource).GetTypeInfo().Assembly.GetManifestResourceStream(name);
        }
    }
}
