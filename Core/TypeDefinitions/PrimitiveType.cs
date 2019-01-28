using System;

namespace Core.TypeDefinitions
{
    public abstract class PrimitiveType
    {
        public abstract Type InternalType { get; }
    }
}
