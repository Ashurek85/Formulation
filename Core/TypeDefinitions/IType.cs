using System;

namespace Core.TypeDefinitions
{
    public interface IType<TResult>
    {
        Type InternalType { get; }
    }
}
