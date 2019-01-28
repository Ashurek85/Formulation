using Core.Blocks.Elementals;
using Core.Blocks.Elementals.Composites;
using Core.TypeDefinitions;

namespace Core.Blocks.Comparison
{
    public abstract class BaseComparison<TOperands> : Binary<TOperands, BoolType>
        where TOperands : PrimitiveType
    {
        public BaseComparison()
        {
        }

        public BaseComparison(Block<TOperands> leftOperand, Block<TOperands> rightOperand)
            : base(leftOperand, rightOperand)
        {
        }
    }
}
