using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableDecimalGraph : ISingleNullableValueGraph<decimal>
    {
        public Decimal? Value { get; set; }
    }
}