using System;

namespace Core.TypeDefinitions.NumericTypes
{
    public abstract class NumericType<TResult> : PrimitiveNumericType, IType<TResult>
    {
        public override Type InternalType => typeof(TResult);
    }
}
