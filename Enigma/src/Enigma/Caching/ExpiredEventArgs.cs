namespace Enigma.Caching
{
    public class ExpiredEventArgs<TKey, TContent>
    {
        public ICacheContent<TKey, TContent> Content { get; }
        public bool CancelEviction { get; set; }

        public ExpiredEventArgs(ICacheContent<TKey, TContent> content)
        {
            Content = content;
        }
    }
}