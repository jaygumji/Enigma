namespace Enigma.Serialization.Json
{
    /// <summary>
    /// Passes through the name from the serialization framework,
    /// most often the property name.
    /// </summary>
    public class BasicFieldNameResolver : IFieldNameResolver
    {
        public string Resolve(VisitArgs args)
        {
            return args.Name;
        }
    }
}