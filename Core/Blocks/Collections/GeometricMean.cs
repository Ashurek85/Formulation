using Core.Blocks.Elementals;
using Core.Introspection;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using Core.TypeDefinitions.GenericCapabilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Collections
{
    [SerializationValues(nameof(TArigmeticType), new Type[] { typeof(IArigmeticType) })]
    public class GeometricMean<TArigmeticType> : ProjectionBlock<TArigmeticType>
        where TArigmeticType : PrimitiveType, IArigmeticType, new()
    {
        public GeometricMean()
        {

        }

        public GeometricMean(string selectedProperty)
            : base(selectedProperty)
        {

        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // Extract underlying type of List
            Type underlyingTypeList = paramExpression.Type.GenericTypeArguments.First();
            // Create a new parameter
            ParameterExpression newParameter = Expression.Parameter(underlyingTypeList, "p");
            // Create generic IEnumerable<>
            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);

            MethodInfo geoMethod = GetType().GetMethod(nameof(GetGeometricMean), BindingFlags.NonPublic | BindingFlags.Static);

            Expression geoExpression = null;

            if (!string.IsNullOrEmpty(SelectedProperty))
            {
                // Geo(p => p.selectedProperty)
                Type propertyType = ClassMetadataLocator.GetPropertyType(underlyingTypeList, SelectedProperty);

                Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, propertyType);

                // Proyección Select
                Expression selectExpression = Expression.Lambda(Expression.Property(newParameter, SelectedProperty), newParameter);
                MethodInfo selectMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Select), new[] { underlyingTypeList, propertyType }, new[] { ienumerableType, funcType }, BindingFlags.Static);

                Expression projectionExpression = Expression.Call(selectMethod,
                                                                  paramExpression,
                                                                  selectExpression);

                geoExpression = Expression.Call(geoMethod, projectionExpression);

            }
            else
            {
                // Geo(IEnumerable<PrimitiveType>)                
                geoExpression = Expression.Call(geoMethod, paramExpression);
            }

            Type internalType = new TArigmeticType().InternalType;
            if (geoExpression.Type != internalType)
                return Expression.Convert(geoExpression, internalType);

            return geoExpression;
        }

        private static double GetGeometricMean(IEnumerable items)
        {
            double product = 1;
            int cont = 0;
            foreach (object item in items)
            {
                product = product * Convert.ToDouble(item);
                cont++;
            }

            return Math.Pow(product, (double)1 / cont);
        }
    }
}