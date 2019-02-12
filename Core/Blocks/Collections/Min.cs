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
    public class Min<TArithmeticType> : ProjectionBlock<TArithmeticType>
        where TArithmeticType : PrimitiveType, IArithmeticType, new()
    {

        public Min()
        {

        }

        public Min(string selectedProperty)
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

            Expression minExpression = null;

            if (!string.IsNullOrEmpty(SelectedProperty))
            {
                // Max(p => p.NombrePropiedad)
                Type propertyType = ClassMetadataLocator.GetPropertyType(underlyingTypeList, SelectedProperty);

                Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, propertyType);

                MethodInfo maxMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Min), new[] { underlyingTypeList }, new[] { ienumerableType, funcType }, BindingFlags.Static);

                Expression propertyProjection = Expression.Lambda(Expression.Property(newParameter, SelectedProperty),
                                                           newParameter);
                minExpression = Expression.Call(maxMethod, paramExpression, propertyProjection);
            }
            else
            {
                // Max(IEnumerable<PrimitiveType>)
                MethodInfo maxMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Min), new[] { underlyingTypeList }, new[] { ienumerableType }, BindingFlags.Static);
                minExpression = Expression.Call(maxMethod, paramExpression);
            }

            Type internalType = new TArithmeticType().InternalType;
            if (minExpression.Type != internalType)
                return Expression.Convert(minExpression, internalType);

            return minExpression;
        }
    }
}