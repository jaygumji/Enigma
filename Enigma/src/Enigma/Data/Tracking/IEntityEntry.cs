using System;

namespace Enigma.Data.Tracking
{
    /// <summary>
    /// Entity entry representing the current state of the entity
    /// </summary>
    public interface IEntityEntry
    {
        /// <summary>Gets the key.</summary>
        /// <value>The key.</value>
        object Key { get; }

        /// <summary>Gets the entity.</summary>
        /// <value>The entity.</value>
        object Entity { get; }

        /// <summary>Gets the type of the entity.</summary>
        /// <value>The type of the entity.</value>
        Type EntityType { get; }

        /// <summary>Gets the state.</summary>
        /// <value>The state.</value>
        EntityState State { get; }
    }
}