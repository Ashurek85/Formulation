using Core.TypeDefinitions;

namespace Core.Blocks.Constants
{
    public class StringConstant : ConstantBlock<StringType, string>
    {
        public StringConstant()
        {
        }

        public StringConstant(string value)
            : base(value)
        {
        }

    }
}
