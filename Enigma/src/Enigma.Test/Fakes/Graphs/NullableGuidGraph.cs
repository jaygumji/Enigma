using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableGuidGraph : ISingleNullableValueGraph<Guid>
    {
        public Guid? Value { get; set; }
    }
}