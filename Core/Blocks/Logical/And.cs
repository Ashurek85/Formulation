using System.Linq.Expressions;
using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;

namespace Core.Blocks.Logical
{
    /// <summary>
    /// And Operator. Only with boolean
    /// </summary>
    public class And : Binary<BoolType, BoolType>
    {

        public And()
        {

        }

        public And(Block<BoolType> leftOperand, Block<BoolType> rightOperand)
            : base(leftOperand, rightOperand)
        {

        }

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.AndAlso(leftExpression, rightExpression);
        }
    }
}
