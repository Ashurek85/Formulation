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
    public class Concat : ProjectionBlock<StringType>
    {
        public string Delimiter { get; set; }

        public Concat()
        {

        }

        public Concat(string selectedProperty, string delimiter)
            :base(selectedProperty)
        {
            Delimiter = delimiter;
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            ParameterExpression paramExpressionI = Expression.Parameter(typeof(string), "i");
            ParameterExpression paramExpressionJ = Expression.Parameter(typeof(string), "j");

            MethodInfo stringConcat = typeof(string).GetMethod(nameof(string.Concat), new Type[] { typeof(string), typeof(string), typeof(string) });

            // Concatenación
            Expression concatExpression = Expression.Call(stringConcat,
                                                          paramExpressionI, Expression.Constant(Delimiter), paramExpressionJ);

            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(typeof(string));
            Type funcType = typeof(Func<,,>).MakeGenericType(typeof(string), typeof(string), typeof(string));

            // public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
            MethodInfo aggregateMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Aggregate), new[] { typeof(string) }, new[] { ienumerableType, funcType }, BindingFlags.Static);

            // Select
            // public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector);
            Type underlyingTypeList = paramExpression.Type.GenericTypeArguments.First();
            ParameterExpression newParameter = Expression.Parameter(underlyingTypeList, "newParameter");

            Expression selectExpression = Expression.Lambda(Expression.Property(newParameter, SelectedProperty), newParameter);
            MethodInfo selectMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable),
                                                                                        nameof(Enumerable.Select),
                                                                                        new[] { underlyingTypeList, typeof(string) },
                                                                                        new[] { paramExpression.Type, typeof(Func<,>).MakeGenericType(underlyingTypeList, typeof(string)) },
                                                                                        BindingFlags.Static);

            Expression projection = Expression.Call(selectMethod,
                                                    paramExpression,
                                                    selectExpression);

            return Expression.Call(aggregateMethod,
                                   projection,
                                   Expression.Lambda(concatExpression, paramExpressionI, paramExpressionJ));

        }
    }
}
