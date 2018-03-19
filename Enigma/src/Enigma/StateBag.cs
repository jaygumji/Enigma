using System;
using System.Collections.Generic;

namespace Enigma
{
    /// <summary>
    /// A bag of stateBag
    /// </summary>
    public class StateBag : IReadOnlyStateBag
    {

        public static IReadOnlyStateBag Empty { get; } = new StateBag();


        private readonly Dictionary<string, object> _values;

        /// <summary>
        /// Creates a new instance of <see cref="StateBag"/>
        /// </summary>
        public StateBag()
        {
            _values = new Dictionary<string, object>();
        }

        /// <summary>
        /// Sets a value in the list
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <param name="value">The value of the argument</param>
        public void Set(string name, object value)
        {
            _values[name] = value;
        }

        /// <summary>
        /// Gets a value in the list
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <returns>The argument value</returns>
        /// <exception cref="ArgumentNotFoundException">Thrown when the argument with the given name was not found</exception>
        public object Get(string name)
        {
            if (!_values.TryGetValue(name, out var value))
                throw new ArgumentNotFoundException(name);

            return value;
        }

        /// <inheritdoc />
        public bool TryGetValue(string name, out object value)
        {
            return _values.TryGetValue(name, out value);
        }

        /// <summary>
        /// Sets a value in the list
        /// </summary>
        /// <typeparam name="T">The type of the argument value</typeparam>
        /// <param name="value">The value of the argument</param>
        public void Set<T>(T value)
        {
            var name = typeof (T).FullName;
            _values[name] = value;;
        }

        /// <inheritdoc />
        public T Get<T>()
        {
            var name = typeof(T).FullName;
            return (T) Get(name);
        }

        /// <inheritdoc />
        public bool TryGetValue<T>(out T value)
        {
            var name = typeof(T).FullName;

            if (!_values.TryGetValue(name, out var untypedValue)) {
                value = default(T);
                return false;
            }

            value = (T) untypedValue;
            return true;
        }

    }
}
