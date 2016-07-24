using System.Collections.Generic;

namespace Enigma.Db.Modelling
{

    public class EnigmaSchemaEntity
    {

        public EnigmaSchemaEntity()
        {
            Properties = new List<EnigmaSchemaEntityProperty>();
            Indexes = new List<EnigmaSchemaEntityIndex>();
        }

        public string Name { get; set; }

        public List<EnigmaSchemaEntityProperty> Properties { get; }

        public List<EnigmaSchemaEntityIndex> Indexes { get; } 
        
    }
}
