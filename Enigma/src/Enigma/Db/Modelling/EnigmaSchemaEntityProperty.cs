using Enigma.Reflection;

namespace Enigma.Db.Modelling
{
    public class EnigmaSchemaEntityProperty
    {
        public string Name { get; set; }
        public int FieldIndex { get; set; }
        public TypeClassification TypeClassification { get; set; }
        public StrictValueType? Type { get; set; }
        public string CustomType { get; set; }
    }
}