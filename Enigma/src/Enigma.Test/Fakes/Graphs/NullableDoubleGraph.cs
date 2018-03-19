using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableDoubleGraph : ISingleNullableValueGraph<double>
    {
        public Double? Value { get; set; }
    }
}