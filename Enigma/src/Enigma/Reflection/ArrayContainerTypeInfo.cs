using System;

namespace Enigma.Reflection
{
    public class ArrayContainerTypeInfo : CollectionContainerTypeInfo
    {
        public int Ranks { get; }

        public ArrayContainerTypeInfo(Type elementType, int ranks) : base(elementType)
        {
            Ranks = ranks;
        }
    }
}