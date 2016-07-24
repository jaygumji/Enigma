namespace Enigma.Db
{
    /// <summary>
    /// A key extractor extracts the key from entities
    /// </summary>
    public interface IKeyExtractor
    {
        //byte[] ExtractBytes(object entity);

        /// <summary>Extracts the key as a value.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The key</returns>
        object ExtractValue(object entity);
    }
}