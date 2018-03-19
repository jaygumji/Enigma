using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableInt32Graph : ISingleNullableValueGraph<Int32>
    {
        public Int32? Value { get; set; }
    }
}