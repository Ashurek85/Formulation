using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Operators
{
    public class Multiply : Binary<NumericType, NumericType>
    {

        public Multiply()
        {
        }

        public Multiply(Block<NumericType> leftExpression, Block<NumericType> rightExpression)
            : base(leftExpression, rightExpression)
        {
        }


        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Multiply(leftExpression, rightExpression);
        }
    }
}