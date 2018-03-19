using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableInt16Graph : ISingleNullableValueGraph<short>
    {
        public Int16? Value { get; set; }
    }
}