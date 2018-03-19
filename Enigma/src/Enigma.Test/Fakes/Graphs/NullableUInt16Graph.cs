using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableUInt16Graph : ISingleNullableValueGraph<UInt16>
    {
        public UInt16? Value { get; set; }
    }
}