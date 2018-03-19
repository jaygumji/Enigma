using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableUInt64Graph : ISingleNullableValueGraph<UInt64>
    {
        public UInt64? Value { get; set; }
    }
}