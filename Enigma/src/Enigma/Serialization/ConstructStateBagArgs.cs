using Enigma.Serialization.Reflection;

namespace Enigma.Serialization
{
    /// <summary>
    /// Arguments available when reading additional attributes from properties
    /// </summary>
    public class ConstructStateBagArgs
    {
        /// <summary>
        /// Information about the property we're constructing arguments for
        /// </summary>
        public SerializableProperty Property { get; }

        /// <summary>
        /// A state bag where we can store information that will be available to visitors
        /// </summary>
        public StateBag StateBag { get; }

        /// <summary>
        /// The level type which will be displayed in the visitor
        /// </summary>
        public LevelType LevelType { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ConstructStateBagArgs"/>
        /// </summary>
        /// <param name="property"></param>
        /// <param name="stateBag"></param>
        /// <param name="levelType"></param>
        public ConstructStateBagArgs(SerializableProperty property, StateBag stateBag, LevelType levelType)
        {
            Property = property;
            StateBag = stateBag;
            LevelType = levelType;
        }
    }
}