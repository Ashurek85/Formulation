using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Constants
{
    public abstract class ConstantBlock<TPrimitiveType, TValue> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
    {
        public TValue Value { get; set; }

        public ConstantBlock()
        {

        }

        public ConstantBlock(TValue value)
        {
            Value = value;
        }

        public override Expression BuildExpression(ParameterExpression paramDatos)
        {
            return Expression.Constant(Value);
        }
    }
}
