using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.DataContext
{
    /// <summary>
    /// Move data context to another property. PropertyName must be of the underlying type of TPrimitiveType
    /// </summary>
    /// <typeparam name="TPrimitiveType"></typeparam>
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class DataNavigation<TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType
    {

        public string PropertyName { get; set; }

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

        public DataNavigation()
        {

        }

        public DataNavigation(string propertyName, Block<TPrimitiveType> internalBlock)
        {
            PropertyName = propertyName;
            InternalBlock = internalBlock;
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // Expression to property
            MemberExpression propiedad = Expression.Property(paramExpression, PropertyName);

            ParameterExpression newContext = Expression.Variable(propiedad.Type, "newContext");
            return Expression.Block(new ParameterExpression[] { newContext },
                                    Expression.Assign(newContext, propiedad),
                                    InternalBlock.BuildExpression(newContext));
        }
    }
}
