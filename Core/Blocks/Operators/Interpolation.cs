using Core.Blocks.Elementals;
using Core.TypeDefinitions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Blocks.Operators
{
    public class Interpolation : Block<NumericType>
    {

        private Block<NumericType> operand1;
        public Block<NumericType> Operand1
        {
            get => operand1;
            set
            {
                operand1 = value;
                if (operand1 != null)
                    operand1.Parent = this;
            }
        }

        private Block<NumericType> operand2;
        public Block<NumericType> Operand2
        {
            get => operand2;
            set
            {
                operand2 = value;
                if (operand2 != null)
                    operand2.Parent = this;
            }
        }

        private Block<NumericType> operand3;
        public Block<NumericType> Operand3
        {
            get => operand3;
            set
            {
                operand3 = value;
                if (operand3 != null)
                    operand3.Parent = this;
            }
        }

        private Block<NumericType> operand4;
        public Block<NumericType> Operand4
        {
            get => operand4;
            set
            {
                operand4 = value;
                if (operand4 != null)
                    operand4.Parent = this;
            }
        }

        private Block<NumericType> operand5;
        public Block<NumericType> Operand5
        {
            get => operand5;
            set
            {
                operand5 = value;
                if (operand5 != null)
                    operand5.Parent = this;
            }
        }
        

        public Interpolation()
        {

        }

        public Interpolation(Block<NumericType> operand1, Block<NumericType> operand2, Block<NumericType> operand3, 
                             Block<NumericType> operand4, Block<NumericType> operand5)
        {
            Operand1 = operand1;
            Operand2 = operand2;
            Operand3 = operand3;
            Operand4 = operand4;
            Operand5 = operand5;
        }

        public override Expression BuildExpression(ParameterExpression paramExpression)
        {
            Expression exp1 = Operand1.BuildExpression(paramExpression);
            Expression exp2 = Operand2.BuildExpression(paramExpression);
            Expression exp3 = Operand3.BuildExpression(paramExpression);
            Expression exp4 = Operand4.BuildExpression(paramExpression);
            Expression exp5 = Operand5.BuildExpression(paramExpression);

            return Expression.Condition(Expression.Equal(Expression.Subtract(exp3, exp1), Expression.Constant(0D)),
                                        Expression.Constant(0D),
                                        Expression.Add(exp2, Expression.Multiply(
                                                                    Expression.Divide(
                                                                                Expression.Subtract(exp4, exp2),
                                                                                Expression.Subtract(exp3, exp1)),
                                                                    Expression.Subtract(exp5, exp1))));
        }
    }
}
