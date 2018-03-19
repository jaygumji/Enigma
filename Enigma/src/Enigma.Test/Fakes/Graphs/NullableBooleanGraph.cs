using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableBooleanGraph : ISingleNullableValueGraph<bool>
    {
        public Boolean? Value { get; set; }
    }
}