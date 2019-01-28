using Core.Blocks.Elementals;
using Core.Introspection;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using Core.TypeDefinitions.GenericCapabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Collections
{
    [SerializationValues(nameof(TArigmeticType), new Type[] { typeof(IArigmeticType) })]
    public class Average<TArigmeticType> : ProjectionBlock<TArigmeticType>
        where TArigmeticType : PrimitiveType, IArigmeticType, new()
    {
        public Average()
        {

        }

        public Average(string selectedProperty)
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
            Type iEnumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);

            Expression avgExpression = null;

            if (!string.IsNullOrEmpty(SelectedProperty))
            {
                // Avg(p => p.NombrePropiedad)
                Type propertyType = ClassMetadataLocator.GetPropertyType(underlyingTypeList, SelectedProperty);

                Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, propertyType);

                MethodInfo averageMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Average), new[] { underlyingTypeList }, new[] { iEnumerableType, funcType }, BindingFlags.Static);

                Expression propertyProjection = Expression.Lambda(Expression.Property(newParameter, SelectedProperty),
                                                           newParameter);

                avgExpression = Expression.Call(averageMethod, paramExpression, propertyProjection);
            }
            else
            {
                // Avg(IEnumerable<PrimitiveType>)    
                MethodInfo avgMethod = typeof(Enumerable).GetMethods().Single(m => m.Name == nameof(Enumerable.Average) &&
                                                                                   m.GetParameters().Length == 1 &&
                                                                                   m.GetParameters()[0].ParameterType == iEnumerableType);
                avgExpression = Expression.Call(avgMethod, paramExpression);
            }

            Type internalType = new TArigmeticType().InternalType;
            if (avgExpression.Type != internalType)
                return Expression.Convert(avgExpression, internalType);

            return avgExpression;            
        }
    }
}
