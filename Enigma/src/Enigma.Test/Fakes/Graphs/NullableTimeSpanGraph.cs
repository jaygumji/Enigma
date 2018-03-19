using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableTimeSpanGraph : ISingleNullableValueGraph<TimeSpan>
    {
        public TimeSpan? Value { get; set; }
    }
}