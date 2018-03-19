using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableDateTimeGraph : ISingleNullableValueGraph<DateTime>
    {
        public DateTime? Value { get; set; }
    }
}