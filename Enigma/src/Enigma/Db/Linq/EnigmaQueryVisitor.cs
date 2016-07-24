using System.Linq.Expressions;

namespace Enigma.Db.Linq
{
    public class EnigmaQueryVisitor : ExpressionVisitor
    {
        private EnigmaQueryStack _stack;

        public EnigmaQueryVisitor(EnigmaQueryStack stack)
        {
            _stack = stack;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            return base.VisitBinary(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            return base.VisitMember(node);
        }
    }
}