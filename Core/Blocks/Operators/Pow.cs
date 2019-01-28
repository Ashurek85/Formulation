using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;
using System.Linq.Expressions;


namespace Core.Blocks.Operators
{
    public class Pow : Binary<NumericType, NumericType>
    {

        public Pow()
        {
        }

        public Pow(Block<NumericType> leftExpression, Block<NumericType> rightExpression)
            : base(leftExpression, rightExpression)
        {
        }


        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Power(leftExpression, rightExpression);
        }
    }
}