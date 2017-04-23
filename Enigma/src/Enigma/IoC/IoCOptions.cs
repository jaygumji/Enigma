namespace Enigma.IoC
{
    public class IoCOptions
    {
        public static readonly IoCOptions Default = new IoCOptions();

        public bool IncludeAllInterfaces { get; set; }
        public bool IncludeAllBaseTypes { get; set; }
        public bool AllowOutsideScope { get; set; }
    }
}