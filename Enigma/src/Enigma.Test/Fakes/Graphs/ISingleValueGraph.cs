using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public interface ISingleValueGraph<T>
        where T : IComparable<T>
    {
        T Value { get; }
    }
}