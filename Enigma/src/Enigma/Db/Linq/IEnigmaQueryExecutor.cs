using System.Linq.Expressions;

namespace Enigma.Db.Linq
{
    public interface IEnigmaQueryExecutor
    {
        object Execute(Expression expression, bool isCollection);
    }
}