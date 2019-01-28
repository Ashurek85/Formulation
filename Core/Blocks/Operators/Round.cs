using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Core.Blocks.Operators
{
    public class Round : Block<NumericType>
    {
        private Block<NumericType> operand;
        public Block<NumericType> Operand
        {
            get => operand;
            set
            {
                operand = value;
                if (operand != null)
                    operand.Parent = this;
            }
        }

        public int DecimalPrecision { get; set; }

        private static MethodInfo roundMethod;
        private static MethodInfo RoundMethod
        {
            get
            {
                if (roundMethod == null)
                {
                    // Find overload
                    // public static double Round(double value, int digits)
                    roundMethod = typeof(Math).GetMethods().Single(m => m.Name == nameof(Math.Round) &&
                                                                        m.GetParameters().Length == 2 &&
                                                                        m.GetParameters()[0].ParameterType == typeof(double) &&
                                                                        m.GetParameters()[1].ParameterType == typeof(int) &&
                                                                        m.ReturnType == typeof(double));
                }
                return roundMethod;
            }
        }

        public Round()
        {
        }

        public Round(Block<NumericType> operand, int decimalPrecision)
        {
            Operand = operand;
            DecimalPrecision = decimalPrecision;
        }

        public override Expression BuildExpression(ParameterExpression paramDatos)
        {
            Expression internalExpression = Operand.BuildExpression(paramDatos);
            Expression roundExpression = null;
            if (internalExpression.Type == typeof(double))
                roundExpression = Expression.Call(RoundMethod, internalExpression, Expression.Constant(DecimalPrecision));
            else
            {
                // Automatic conversion
                roundExpression = Expression.Call(RoundMethod, Expression.Convert(internalExpression, typeof(double)),
                                                        Expression.Constant(DecimalPrecision));
            }

            return roundExpression;
        }
    }
}
