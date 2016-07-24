using System.Linq.Expressions;

namespace Enigma.Db.Linq
{

    public class EnigmaQueryExecutor : IEnigmaQueryExecutor
    {
        private readonly IEnigmaConnection _connection;

        public EnigmaQueryExecutor(IEnigmaConnection connection)
        {
            _connection = connection;
        }

        public object Execute(Expression expression, bool isCollection)
        {
            var stack = new EnigmaQueryStack();

            var visitor = new EnigmaQueryVisitor(stack);
            visitor.Visit(expression);

            return null;
        }
    }
}