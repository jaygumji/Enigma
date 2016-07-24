using System.Linq.Expressions;
using Enigma.Db.Linq;

namespace Enigma.Test.Db.Linq
{
    public class FakeEnigmaQueryExecutor : IEnigmaQueryExecutor
    {

        public object Execute(Expression expression, bool isCollection)
        {
            return null;
        }
    }
}