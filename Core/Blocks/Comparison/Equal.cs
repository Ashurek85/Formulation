using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Comparison
{
    [SerializationValues(nameof(TOperands), new Type[] { typeof(PrimitiveType) })]
    public class Equal<TOperands> : BaseComparison<TOperands>
        where TOperands : PrimitiveType
    {

        public Equal()
        {

        }

        public Equal(Block<TOperands> leftOperand, Block<TOperands> rightOperand)
            : base(leftOperand, rightOperand)
        {

        }

        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Equal(leftExpression, rightExpression);
        }
    }
}
