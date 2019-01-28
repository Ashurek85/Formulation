using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Comparison
{
    [SerializationValues(nameof(TOperands), new Type[] { typeof(PrimitiveType) })]
    public class GreaterThanOrEqual<TOperands> : BaseComparison<TOperands>
        where TOperands : PrimitiveType
    {

        public GreaterThanOrEqual()
        {

        }

        public GreaterThanOrEqual(Block<TOperands> leftOperand, Block<TOperands> rightOperand)
            : base(leftOperand, rightOperand)
        {

        }

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.GreaterThanOrEqual(leftExpression, rightExpression);
        }
    }
}
