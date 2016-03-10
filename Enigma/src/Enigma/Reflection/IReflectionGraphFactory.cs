using System;

namespace Enigma.Reflection
{
    /// <summary>
    /// Interface IReflectionGraphFactory
    /// </summary>
    public interface IReflectionGraphFactory
    {
        /// <summary>
        /// Tries to get the property visitor.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="visitor">The visitor.</param>
        /// <returns><c>true</c> if visitor available, <c>false</c> otherwise.</returns>
        bool TryCreatePropertyVisitor(Type type, out IReflectionGraphPropertyVisitor visitor);
    }
}
