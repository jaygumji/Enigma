using System.Linq;

namespace Enigma.Collections
{
    public interface IQueryableSet : IQueryable
    {
    }

    public interface IQueryableSet<T> : IQueryable<T>, IQueryableSet
    {
        void Add(T entity);
        void Remove(T entity);

        T Get(object key);
        bool TryGet(object key, out T entity);
    }
}
