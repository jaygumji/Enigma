namespace Enigma.Serialization.Json
{
    /// <summary>
    /// Passes through the property name.
    /// </summary>
    public class PropertyNameFieldNameResolver : IFieldNameResolver
    {
        public string Resolve(VisitArgs args)
        {
            return !string.IsNullOrEmpty(args.Attributes.Name)
                ? args.Attributes.Name
                : args.Name;
        }
    }
}