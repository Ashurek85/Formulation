using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System.Linq;
using System.Reflection;
using Core.Introspection;
using System.Collections;
using Core.Introspection.Attributes;

namespace Core.Blocks.Collections
{
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class First<TPrimitiveType> : ProjectionBlock<TPrimitiveType>
        where TPrimitiveType : PrimitiveType, new()
    {

        public First()
        {
        }

        public First(string selectedProperty)
            : base(selectedProperty)
        {
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // Get underlying type of IEnumerable
            if (!paramExpression.Type.GetInterfaces().Contains(typeof(IEnumerable)))
                throw new Exception($"DataContext (paramExpression: {paramExpression.Type.ToString()}) must implement IEnumerable. Where apply over IEnumerable<T>");

            if (paramExpression.Type.GenericTypeArguments.Length == 0)
            {
                throw new Exception($"DataContext (paramExpression: {paramExpression.Type.ToString()}) must have one generic parameter: IEnumerable<T>");
            }

            Type underlyingTypeList = paramExpression.Type.GenericTypeArguments.First();
            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);

            MethodInfo firstMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.FirstOrDefault), new[] { underlyingTypeList }, new[] { ienumerableType }, BindingFlags.Static); ;
            Expression firstExpression = Expression.Call(firstMethod, paramExpression);

            Expression propertyExpression = Expression.Property(firstExpression, SelectedProperty);

            // Check types
            Type internalType = new TPrimitiveType().InternalType;
            if (propertyExpression.Type != internalType)
                return Expression.Convert(propertyExpression, internalType);
            else
                return propertyExpression;
        }
    }
}
