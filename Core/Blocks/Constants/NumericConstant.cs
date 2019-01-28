using Core.TypeDefinitions;

namespace Core.Blocks.Constants
{
    public class NumericConstant : ConstantBlock<NumericType, double>
    {

        public NumericConstant()
        {

        }

        public NumericConstant(double value)
            : base(value)
        {

        }

    }
}
