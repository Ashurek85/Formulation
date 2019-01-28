using Core.Blocks.Elementals;
using Core.Introspection;
using Core.TypeDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Collections
{
    public class Count : Block<NumericType>
    {

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            // Extract underlying type of List
            Type underlyingTypeList = paramExpression.Type.GenericTypeArguments.First();
            // Create a new parameter
            ParameterExpression newParameter = Expression.Parameter(underlyingTypeList, "newParameter");
            // Create generic IEnumerable<>
            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);

            // Count()
            MethodInfo countMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Count), new[] { underlyingTypeList }, new[] { ienumerableType }, BindingFlags.Static);
            return Expression.Convert(Expression.Call(countMethod, paramExpression), typeof(double)); // Auto conversion to double
        }
    }
}
