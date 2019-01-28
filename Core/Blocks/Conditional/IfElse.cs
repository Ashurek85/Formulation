using Core.Blocks.Comparison;
using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.Conditional
{
    [SerializationValues(nameof(TComparison), new Type[] { typeof(PrimitiveType) })]
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class IfElse<TComparison, TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
        where TComparison : PrimitiveType
    {

        private BaseComparison<TComparison> comparison;
        public BaseComparison<TComparison> Comparison
        {
            get => comparison;
            set
            {
                comparison = value;
                if (comparison != null)
                    comparison.Parent = this;
            }
        }

        private Block<TPrimitiveType> trueBlock;
        public Block<TPrimitiveType> True
        {
            get => trueBlock;
            set
            {
                trueBlock = value;
                if (trueBlock != null)
                    trueBlock.Parent = this;
            }
        }

        private Block<TPrimitiveType> falseBlock;
        public Block<TPrimitiveType> False
        {
            get => falseBlock;
            set
            {
                falseBlock = value;
                if (falseBlock != null)
                    falseBlock.Parent = this;
            }
        }

        public IfElse()
        {

        }

        public IfElse(BaseComparison<TComparison> comparison, Block<TPrimitiveType> trueBlock, Block<TPrimitiveType> falseBlock)
        {
            Comparison = comparison;
            True = trueBlock;
            False = falseBlock;
        }


        public override Expression BuildExpression(ParameterExpression paramDatos)
        {
            // Generate expressions
            Expression comparisonExpression = Comparison.BuildExpression(paramDatos);
            Expression trueExpression = True.BuildExpression(paramDatos);
            Expression falseExpression = False.BuildExpression(paramDatos);

            // Build if-else expression
            return Expression.Condition(comparisonExpression, trueExpression, falseExpression);
        }
    }
}
