using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class NullableSingleGraph : ISingleNullableValueGraph<float>
    {
        public Single? Value { get; set; }
    }
}