using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public interface ISingleNullableValueGraph<T>
        where T : struct, IComparable<T>
    {
        T? Value { get; }
    }
}