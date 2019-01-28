using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Logical
{
    /// <summary>
    /// Or Operator. Only with boolean
    /// </summary>
    public class Or : Binary<BoolType, BoolType>
    {

        public Or()
        {

        }

        public Or(Block<BoolType> leftOperand, Block<BoolType> rightOperand)
            : base(leftOperand, rightOperand)
        {

        }

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.OrElse(leftExpression, rightExpression);
        }
    }
}
