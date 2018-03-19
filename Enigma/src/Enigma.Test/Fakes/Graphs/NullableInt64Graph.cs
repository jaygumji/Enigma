using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableInt64Graph : ISingleNullableValueGraph<Int64>
    {
        public Int64? Value { get; set; }
    }
}