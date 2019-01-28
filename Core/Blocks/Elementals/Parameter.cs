using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Elementals
{
    public class Parameter<TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
    {
        public object Value { get; set; }        

        public Parameter()
        {

        }        

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            return Expression.Constant(Value);
        }
    }
}
