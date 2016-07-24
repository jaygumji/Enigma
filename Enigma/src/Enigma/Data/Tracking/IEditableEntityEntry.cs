namespace Enigma.Data.Tracking
{
    /// <summary>
    /// Enables editing of an entity entry
    /// </summary>
    public interface IEditableEntityEntry
    {
        /// <summary>Modifies the state.</summary>
        /// <param name="newState">The new state.</param>
        void ModifyState(EntityState newState);
    }
}