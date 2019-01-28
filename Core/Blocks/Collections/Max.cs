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
    [SerializationValues(nameof(TArigmeticType), new Type[] { typeof(IArigmeticType) })]
    public class Max<TArigmeticType> : ProjectionBlock<TArigmeticType>
        where TArigmeticType : PrimitiveType, IArigmeticType, new()
    {

        public Max()
        {

        }

        public Max(string selectedProperty)
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

            Expression maxExpression = null;

            if (!string.IsNullOrEmpty(SelectedProperty))
            {
                // Max(p => p.NombrePropiedad)
                Type propertyType = ClassMetadataLocator.GetPropertyType(underlyingTypeList, SelectedProperty);

                Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, propertyType);

                MethodInfo maxMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Max), new[] { underlyingTypeList }, new[] { ienumerableType, funcType }, BindingFlags.Static);

                Expression propertyProjection = Expression.Lambda(Expression.Property(newParameter, SelectedProperty),
                                                           newParameter);
                maxExpression = Expression.Call(maxMethod, paramExpression, propertyProjection);
            }
            else
            {
                // Max(IEnumerable<PrimitiveType>)
                MethodInfo maxMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Max), new[] { underlyingTypeList }, new[] { ienumerableType }, BindingFlags.Static);
                maxExpression = Expression.Call(maxMethod, paramExpression);
            }

            Type internalType = new TArigmeticType().InternalType;
            if (maxExpression.Type != internalType)
                return Expression.Convert(maxExpression, internalType);                   

            return maxExpression;
        }
    }
}
