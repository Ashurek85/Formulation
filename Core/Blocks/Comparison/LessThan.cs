using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Comparison
{
    [SerializationValues(nameof(TOperands), new Type[] { typeof(PrimitiveType) })]
    public class LessThan<TOperands> : BaseComparison<TOperands>
        where TOperands : PrimitiveType
    {

        public LessThan()
        {

        }

        public LessThan(Block<TOperands> leftOperand, Block<TOperands> rightOperand)
            : base(leftOperand, rightOperand)
        {

        }

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.LessThan(leftExpression, rightExpression);
        }
    }
}