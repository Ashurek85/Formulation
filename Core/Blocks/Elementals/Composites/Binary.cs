using Core.TypeDefinitions;
using System.Linq.Expressions;

namespace Core.Blocks.Elementals.Composites
{
    public abstract class Binary<TOperands, TPrimitiveType> : Block<TPrimitiveType>
        where TOperands : PrimitiveType
        where TPrimitiveType : PrimitiveType
    {

        private Block<TOperands> leftOperand;
        public Block<TOperands> LeftOperand
        {
            get => leftOperand;
            set
            {
                leftOperand = value;
                leftOperand.Parent = this;
            }
        }

        private Block<TOperands> rightOperand;
        public Block<TOperands> RightOperand
        {
            get => rightOperand;
            set
            {
                rightOperand = value;
                rightOperand.Parent = this;
            }
        }

        public Binary()
        {

        }

        public Binary(Block<TOperands> leftOperand, Block<TOperands> rightOperand)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }

        public override Expression BuildExpression(ParameterExpression paramDatos)
        {
            // Se generan las dos expresiones
            Expression leftExpression = LeftOperand.BuildExpression(paramDatos);
            Expression rightExpression = RightOperand.BuildExpression(paramDatos);            

            // Se construye la expressión binaria y se valida            

            return BuildExpression(leftExpression, rightExpression, paramDatos);
        }

        protected abstract Expression BuildExpression(Expression leftExpression, Expression rightExpression, ParameterExpression paramDatos);
    }
}
