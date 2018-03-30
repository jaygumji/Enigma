namespace Enigma.Serialization
{
    public class RootTraveller
    {
        public IGraphTraveller Traveller { get; }
        public LevelType Level { get; }

        public RootTraveller(IGraphTraveller traveller, LevelType level)
        {
            Traveller = traveller;
            Level = level;
        }
    }
}