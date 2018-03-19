using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableUInt32Graph : ISingleNullableValueGraph<UInt32>
    {
        public UInt32? Value { get; set; }
    }
}