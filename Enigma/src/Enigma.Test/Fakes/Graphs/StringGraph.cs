using System;

namespace Enigma.Testing.Fakes.Graphs
{
    public class StringGraph : ISingleValueGraph<string>
    {
        public String Value { get; set; }
    }
}