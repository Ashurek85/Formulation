using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System.Linq;
using System.Reflection;
using Core.Introspection;
using Core.TypeDefinitions.GenericCapabilities;
using Core.Introspection.Attributes;

namespace Core.Blocks.Collections
{
    [SerializationValues(nameof(TArithmeticType), new Type[] { typeof(IArithmeticType) })]
    public class Sum<TArithmeticType> : ProjectionBlock<TArithmeticType>
        where TArithmeticType : PrimitiveType, IArithmeticType, new()
    {

        public Sum()
        {

        }

        public Sum(string selectedProperty)
            : base(selectedProperty)
        {

        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // Extract underlying type of List
            Type underlyingTypeList = paramExpression.Type.GenericTypeArguments.First();
            // Create a new parameter
            ParameterExpression newParameter = Expression.Parameter(underlyingTypeList, "newParameter");
            // Create generic IEnumerable<>
            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);

            Expression sumExpression = null;

            if (!string.IsNullOrEmpty(SelectedProperty))
            {
                // Sum(p => p.SelectedProperty)
                Type propertyType = ClassMetadataLocator.GetPropertyType(underlyingTypeList, SelectedProperty);

                Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, propertyType);

                MethodInfo sumMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Sum) , new[] { underlyingTypeList }, new[] { ienumerableType, funcType }, BindingFlags.Static);

                Expression propertyProjection = Expression.Lambda(Expression.Property(newParameter, SelectedProperty),
                                                           newParameter);
                sumExpression = Expression.Call(sumMethod, paramExpression, propertyProjection);
            }
            else
            {                
                // Sum(IEnumerable<PrimitiveType>)
                MethodInfo sumMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Sum), new[] { underlyingTypeList }, new[] { ienumerableType }, BindingFlags.Static);
                sumExpression = Expression.Call(sumMethod, paramExpression);
            }

            Type internalType = new TArithmeticType().InternalType;
            if (sumExpression.Type != internalType)
                return Expression.Convert(sumExpression, internalType);

            return sumExpression;
        }
    }
}