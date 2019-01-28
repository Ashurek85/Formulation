using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.DataContext
{
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class GlobalDataNavigation<TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
    {

        private Block<TPrimitiveType> internalBlock;
        public Block<TPrimitiveType> InternalBlock
        {
            get => internalBlock;
            set
            {
                internalBlock = value;
                if (internalBlock != null)
                    internalBlock.Parent = this;
            }
        }

        public GlobalDataNavigation()
        {

        }

        public GlobalDataNavigation(Block<TPrimitiveType> internalBlock)
        {
            InternalBlock = internalBlock;
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // New DataContext
            object dataContext = GetDataContext();
            ParameterExpression newDataContextExpression = Expression.Parameter(dataContext.GetType(), "newDataContext");
            return Expression.Block(new ParameterExpression[] { newDataContextExpression },
                                   Expression.Assign(newDataContextExpression, Expression.Constant(dataContext)),
                                   InternalBlock.BuildExpression(newDataContextExpression));
        }
    }
}
