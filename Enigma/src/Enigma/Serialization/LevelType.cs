using System;

namespace Enigma.Serialization
{
    [Flags]
    public enum LevelType
    {
        Value = 1,
        Single = 2,
        Collection = 4,
        CollectionItem = 8,
        Dictionary = 16,
        DictionaryKey = 32,
        DictionaryValue = 64,

        CollectionInCollection = Collection | CollectionItem,
        DictionaryInCollection = Dictionary | CollectionItem,
        DictionaryInDictionaryKey = Dictionary | DictionaryKey,
        DictionaryInDictionaryValue = Dictionary | DictionaryValue,
        CollectionInDictionaryKey = Collection | DictionaryKey,
        CollectionInDictionaryValue = Collection | DictionaryValue
    }

    public static class LevelTypeExtensions
    {

        public static bool IsCollection(this LevelType type)
        {
            return (type & LevelType.Collection) == LevelType.Collection;
        }

        public static bool IsDictionary(this LevelType type)
        {
            return (type & LevelType.Dictionary) == LevelType.Dictionary;
        }

    }
}