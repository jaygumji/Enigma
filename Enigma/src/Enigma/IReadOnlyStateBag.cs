namespace Enigma
{
    public interface IReadOnlyStateBag
    {

        /// <summary>
        /// Gets a value in the list
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <returns>The argument value</returns>
        /// <exception cref="ArgumentNotFoundException">Thrown when the argument with the given name was not found</exception>
        object Get(string name);

        /// <summary>
        /// Tries to get a value in the list
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <param name="value">The argument value</param>
        /// <returns><c>true</c> if the argument was found, otherwise false</returns>
        bool TryGetValue(string name, out object value);

        /// <summary>
        /// Gets a value in the list
        /// </summary>
        /// <typeparam name="T">The type of the argument value</typeparam>
        /// <returns>The argument value</returns>
        /// <exception cref="ArgumentNotFoundException">Thrown when the argument with the given name was not found</exception>
        T Get<T>();

        /// <summary>
        /// Tries to get a value in the list
        /// </summary>
        /// <typeparam name="T">The type of the argument value</typeparam>
        /// <param name="value">The argument value</param>
        /// <returns><c>true</c> if the argument was found, otherwise false</returns>
        bool TryGetValue<T>(out T value);

    }
}