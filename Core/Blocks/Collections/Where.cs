using Core.Blocks.Comparison;
using Core.Blocks.Elementals;
using Core.Introspection;
using Core.Introspection.Attributes;
using Core.TypeDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Collections
{
    [SerializationValues(nameof(TComparison), new Type[] { typeof(PrimitiveType) })]
    [SerializationValues(nameof(TPrimitiveType), new Type[] { typeof(PrimitiveType) })]
    public class Where<TComparison, TPrimitiveType> : Block<TPrimitiveType>
        where TComparison : PrimitiveType
        where TPrimitiveType : PrimitiveType
    {
        private BaseComparison<TComparison> comparison;
        public BaseComparison<TComparison> Comparison
        {
            get => comparison;
            set
            {
                comparison = value;
                if (comparison != null)
                    comparison.Parent = this;
            }
        }

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

        public Where()
        {

        }

        public Where(BaseComparison<TComparison> comparison, Block<TPrimitiveType> internalBlock)
        {
            Comparison = comparison;
            InternalBlock = internalBlock;
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
            ParameterExpression newParameter = Expression.Parameter(underlyingTypeList, "newParameter");

            Expression comparisonExpression = Expression.Lambda(Comparison.BuildExpression(newParameter), newParameter);

            Type ienumerableType = typeof(IEnumerable<>).MakeGenericType(underlyingTypeList);
            Type funcType = typeof(Func<,>).MakeGenericType(underlyingTypeList, typeof(bool));

            MethodInfo whereMethod = (MethodInfo)ClassMetadataLocator.GetGenericMethod(typeof(Enumerable), nameof(Enumerable.Where), new[] { underlyingTypeList }, new[] { ienumerableType, funcType }, BindingFlags.Static);

            if (InternalBlock == null)
            {
                return Expression.Call(whereMethod,
                                       paramExpression,
                                       comparisonExpression);
            }
            else
            {
                Expression filterExpression = Expression.Call(whereMethod, paramExpression, comparisonExpression);

                ParameterExpression whereParameter = Expression.Variable(filterExpression.Type, "whereParameter");

                return Expression.Block(new ParameterExpression[] { whereParameter },
                                        Expression.Assign(whereParameter, filterExpression),
                                        InternalBlock.BuildExpression(whereParameter)
                );
            }
        }
    }
}
