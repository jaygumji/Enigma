namespace Enigma.Data.Tracking
{
    /// <summary>
    /// Represents the state of an entity
    /// </summary>
    public enum EntityState
    {
        /// <summary>The entity is intact</summary>
        Intact,

        /// <summary>The entity is new</summary>
        New,

        /// <summary>The entity has been modified</summary>
        Modified,

        /// <summary>The entity has been removed</summary>
        Removed
    }
}