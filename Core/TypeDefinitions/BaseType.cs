using System;

namespace Core.TypeDefinitions
{
    public abstract class BaseType<TResult> : PrimitiveType
    {

        public override Type InternalType => typeof(TResult);

    }
}
