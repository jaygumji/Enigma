namespace Enigma.Serialization.Json
{
    /// <summary>
    /// Changes the name of the field to a camel case syntax by
    /// making the first character to lower case.
    /// </summary>
    public class CamelCaseFieldNameResolver : FieldNameResolver
    {
        protected override string OnResolve(VisitArgs args)
        {
            return char.ToLowerInvariant(args.Name[0]) + args.Name.Substring(1);
        }
    }
}