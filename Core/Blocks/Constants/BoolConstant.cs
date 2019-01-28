using Core.TypeDefinitions;

namespace Core.Blocks.Constants
{
    public class BoolConstant : ConstantBlock<BoolType, bool>
    {


        public BoolConstant()
        {

        }

        public BoolConstant(bool value)
            : base(value)
        {
        }
    }
}
