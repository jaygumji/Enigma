using System.Collections.Generic;

namespace Enigma.Db.Modelling
{
    public class EnigmaSchemaEntityIndex
    {
        public string Name { get; set; }
        public List<int> FieldIndexPath { get; set; }
    }
}