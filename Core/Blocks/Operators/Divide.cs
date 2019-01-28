using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Operators
{
    public class Divide : Binary<NumericType, NumericType>
    {

        public Divide()
        {
        }

        public Divide(Block<NumericType> leftExpression, Block<NumericType> rightExpression)
            : base(leftExpression, rightExpression)
        {
        }
        

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Divide(leftExpression, rightExpression);
        }
    }
}
