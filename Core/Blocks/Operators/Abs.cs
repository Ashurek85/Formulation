using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Operators
{
    public class Abs : Block<NumericType>
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

        private static MethodInfo absMethod;
        private static MethodInfo AbsMethod
        {
            get
            {
                if (absMethod == null)
                    absMethod = typeof(Math).GetMethods().Single(m => m.Name == nameof(Math.Abs) &&
                                                                      m.GetParameters().Length == 1 &&
                                                                      m.GetParameters()[0].ParameterType == typeof(double));
                return absMethod;
            }
        }

        public Abs()
        {
        }

        public Abs(Block<NumericType> operand)
        {
            Operand = operand;
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            return Expression.Call(AbsMethod, Operand.BuildExpression(paramExpression));
        }
    }
}
