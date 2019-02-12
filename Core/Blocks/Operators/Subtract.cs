using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using Core.TypeDefinitions.GenericCapabilities;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Operators
{
    [SerializationValues(nameof(TArithmeticType), new Type[] { typeof(IArithmeticType) })]
    public class Subtract<TArithmeticType> : Binary<TArithmeticType, TArithmeticType>
        where TArithmeticType : PrimitiveType, IArithmeticType
    {
        public Subtract()
        {
        }

        public Subtract(Block<TArithmeticType> leftExpression, Block<TArithmeticType> rightExpression)
            : base(leftExpression, rightExpression)
        {
        }


        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Subtract(leftExpression, rightExpression);
        }
    }
}
