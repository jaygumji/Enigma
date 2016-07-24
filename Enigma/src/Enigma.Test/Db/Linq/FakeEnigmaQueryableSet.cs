using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Enigma.Collections;
using Enigma.Db.Linq;

namespace Enigma.Test.Db.Linq
{
    public class FakeEnigmaQueryableSet<T> : IQueryableSet<T>
    {
        public Type ElementType
        {
            get {
                return typeof(T);
            }
        }

        public Expression Expression
        {
            get {
                return Expression.Constant(this);
            }
        }

        public IQueryProvider Provider
        {
            get { return new EnigmaQueryProvider(new FakeEnigmaQueryExecutor()); }
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(object key)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public bool TryGet(object key, out T entity)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
