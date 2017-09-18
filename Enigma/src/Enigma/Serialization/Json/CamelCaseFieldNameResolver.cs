namespace Enigma.Serialization.Json
{
    /// <summary>
    /// Changes the name of the field to a camel case syntax by
    /// making the first character to lower case.
    /// </summary>
    public class CamelCaseFieldNameResolver : IFieldNameResolver
    {
        public string Resolve(VisitArgs args)
        {
            if (!string.IsNullOrEmpty(args.Attributes.Name)) {
                return args.Attributes.Name;
            }
            return char.ToLowerInvariant(args.Name[0]) + args.Name.Substring(1);
        }
    }
}