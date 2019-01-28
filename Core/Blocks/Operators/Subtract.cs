using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using Core.TypeDefinitions.GenericCapabilities;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Operators
{
    [SerializationValues(nameof(TArigmeticType), new Type[] { typeof(IArigmeticType) })]
    public class Subtract<TArigmeticType> : Binary<TArigmeticType, TArigmeticType>
        where TArigmeticType : PrimitiveType, IArigmeticType
    {
        public Subtract()
        {
        }

        public Subtract(Block<TArigmeticType> leftExpression, Block<TArigmeticType> rightExpression)
            : base(leftExpression, rightExpression)
        {
        }


        protected override Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos)
        {
            return Expression.Subtract(leftExpression, rightExpression);
        }
    }
}
