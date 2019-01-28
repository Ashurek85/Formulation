using Core.TypeDefinitions;
using System;

namespace Core.Blocks.Elementals
{
    public abstract class Block<TPrimitiveType> : BaseBlock
        where TPrimitiveType : PrimitiveType
    {
        
    }
}
