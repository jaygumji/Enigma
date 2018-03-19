namespace Enigma.Serialization.Json
{
    public class FieldNameResolver : IFieldNameResolver
    {
        public string Resolve(VisitArgs args)
        {
            if (!string.IsNullOrEmpty(args.Attributes.Name)) {
                return args.Attributes.Name;
            }
            return OnResolve(args);
        }

        protected virtual string OnResolve(VisitArgs args)
        {
            return args.Name;
        }
    }
}