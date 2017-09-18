using Enigma.Serialization.Reflection;

namespace Enigma.Serialization
{
    /// <summary>
    /// Arguments available when reading additional attributes from properties
    /// </summary>
    public class ConstructStateArgs
    {
        /// <summary>
        /// Information about the property we're constructing arguments for
        /// </summary>
        public SerializableProperty Property { get; }

        /// <summary>
        /// Standard attributes loaded by the serialization framework.
        /// </summary>
        public EnigmaSerializationAttributes Attributes { get; }

        /// <summary>
        /// A state bag that will be passed to the visitor when iterating the member
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// The level type which will be displayed in the visitor
        /// </summary>
        public LevelType LevelType { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ConstructStateArgs"/>
        /// </summary>
        /// <param name="property"></param>
        /// <param name="attributes"></param>
        /// <param name="levelType"></param>
        public ConstructStateArgs(SerializableProperty property, EnigmaSerializationAttributes attributes, LevelType levelType)
        {
            Property = property;
            Attributes = attributes;
            LevelType = levelType;
        }
    }
}