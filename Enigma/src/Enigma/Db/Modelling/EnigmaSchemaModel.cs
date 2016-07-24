using System;
using System.Collections.Generic;

namespace Enigma.Db.Modelling
{
    /// <summary>
    /// Enigma schema model
    /// </summary>
    public class EnigmaSchemaModel
    {

        private readonly Dictionary<Type, EnigmaSchemaEntity> _entities; 

        public EnigmaSchemaModel()
        {
            _entities = new Dictionary<Type, EnigmaSchemaEntity>();
        }

        /// <summary>Gets the entities.</summary>
        /// <value>The entities.</value>
        public IEnumerable<EnigmaSchemaEntity> Entities => _entities.Values;

        public EnigmaSchemaEntity SchemaFor(Type entityType)
        {
            return _entities[entityType];
        }
    }
}