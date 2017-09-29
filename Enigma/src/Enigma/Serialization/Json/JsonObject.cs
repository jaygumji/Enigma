using System;
using System.Collections;
using System.Collections.Generic;

namespace Enigma.Serialization.Json
{
    public class JsonObject : JsonPrototype, IEnumerable<KeyValuePair<string, JsonPrototype>>
    {

        private readonly Dictionary<string, JsonPrototype> _fields;

        public JsonObject()
        {
            _fields = new Dictionary<string, JsonPrototype>(StringComparer.Ordinal);
        }

        public void Add(string fieldName, JsonPrototype field)
        {
            _fields.Add(fieldName, field);
        }

        public bool Remove(string fieldName)
        {
            return _fields.Remove(fieldName);
        }

        public IEnumerator<KeyValuePair<string, JsonPrototype>> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
