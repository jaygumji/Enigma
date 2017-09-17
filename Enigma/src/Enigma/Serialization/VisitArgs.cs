using Enigma.Serialization.Reflection;

namespace Enigma.Serialization
{
    public class VisitArgs
    {
        public static readonly VisitArgs CollectionItem = new VisitArgs(LevelType.CollectionItem);
        public static readonly VisitArgs DictionaryKey = new VisitArgs(LevelType.DictionaryKey);
        public static readonly VisitArgs DictionaryValue = new VisitArgs(LevelType.DictionaryValue);
        public static readonly VisitArgs CollectionInCollection = new VisitArgs(LevelType.CollectionInCollection);
        public static readonly VisitArgs DictionaryInCollection = new VisitArgs(LevelType.DictionaryInCollection);
        public static readonly VisitArgs DictionaryInDictionaryKey = new VisitArgs(LevelType.DictionaryInDictionaryKey);
        public static readonly VisitArgs DictionaryInDictionaryValue = new VisitArgs(LevelType.DictionaryInDictionaryValue);
        public static readonly VisitArgs CollectionInDictionaryKey = new VisitArgs(LevelType.CollectionInDictionaryKey);
        public static readonly VisitArgs CollectionInDictionaryValue = new VisitArgs(LevelType.CollectionInDictionaryValue);

        private VisitArgs(LevelType type)
            : this(null, type, 0, Enigma.StateBag.Empty, isRoot: false)
        {
        }

        public VisitArgs(string name, LevelType type)
            : this(name, type, 0, Enigma.StateBag.Empty, isRoot: false)
        {
        }

        public VisitArgs(string name, LevelType type, uint index, IReadOnlyStateBag stateBag)
            : this(name, type, index, stateBag, isRoot: false)
        {
        }

        private VisitArgs(string name, LevelType type, uint index, IReadOnlyStateBag stateBag, bool isRoot)
        {
            Name = name;
            Type = type;
            Index = index;
            StateBag = stateBag;
            IsRoot = isRoot;
        }

        public string Name { get; }
        public LevelType Type { get; }
        public uint Index { get; }
        public bool IsRoot { get; }
        public IReadOnlyStateBag StateBag { get; }

        public override string ToString()
        {
            return string.Concat(Type, " args ", Name, " with index ", Index);
        }

        public static VisitArgs CreateRoot(LevelType type)
        {
            return new VisitArgs(null, type, 1, Enigma.StateBag.Empty, isRoot: true);
        }

    }
}