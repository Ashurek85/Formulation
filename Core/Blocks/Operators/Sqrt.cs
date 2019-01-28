using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Operators
{
    public class Sqrt : Block<NumericType>
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

        private static MethodInfo sqrtMethod;
        private static MethodInfo SqrtMethod
        {
            get
            {
                if (sqrtMethod == null)
                    sqrtMethod = typeof(Math).GetMethod(nameof(Math.Sqrt));
                return sqrtMethod;
            }
        }

        public Sqrt()
        {
        }

        public Sqrt(Block<NumericType> operand)
        {
            Operand = operand;
        }

        public override Expression BuildExpression(ParameterExpression paramDatos)
        {
            return Expression.Call(SqrtMethod, Operand.BuildExpression(paramDatos));
        }
    }
}