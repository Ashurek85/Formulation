using Core.Blocks.Elementals;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;

namespace Core.Blocks.DataContext
{
    /// <summary>
    /// Read data from data context. PropertyName must be of the underlying type of TPrimitiveType
    /// </summary>
    /// <typeparam name="TPrimitiveType"></typeparam>
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class ReadData<TPrimitiveType> : Block<TPrimitiveType>
        where TPrimitiveType : PrimitiveType, new()
    {
        public string PropertyName { get; set; }

        public ReadData()
        {
        }

        public ReadData(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override Expression BuildExpression(ParameterExpression dataParameter)
        {
            Expression propertyExpression = Expression.Property(dataParameter, PropertyName);

            // Check types
            Type internalType = new TPrimitiveType().InternalType;
            if (propertyExpression.Type != internalType)
                return Expression.Convert(propertyExpression, internalType);
            else
                return propertyExpression;
        }
    }
}
