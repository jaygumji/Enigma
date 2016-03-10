using System.Reflection;

namespace Enigma.Reflection
{
    /// <summary>
    /// Interface IReflectionGraphPropertyVisitor
    /// </summary>
    public interface IReflectionGraphPropertyVisitor
    {
        /// <summary>
        /// Visits the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        void Visit(PropertyInfo property);
    }
}